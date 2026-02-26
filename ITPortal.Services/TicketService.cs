using AutoMapper;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System.Text.Json;

namespace ITPortal.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly ILookupRepository _lookupRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IConfigurationItemsRepository _configurationItemsRepository;
        private readonly ITicketEventRepository _ticketEventRepository;
        private readonly ITicketAssignmentHistoryRepository _ticketAssignmentHistoryRepository;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IUserRepository userRepository, ITicketCategoryRepository ticketCategoryRepository, ILookupRepository lookupRepository, ITeamRepository teamRepository, IConfigurationItemsRepository configurationItemsRepository, ITicketEventRepository ticketEventRepository, ITicketAssignmentHistoryRepository ticketAssignmentHistoryRepository, ITicketCommentRepository ticketCommentRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
            _lookupRepository = lookupRepository;
            _teamRepository = teamRepository;
            _configurationItemsRepository = configurationItemsRepository;
            _ticketEventRepository = ticketEventRepository;
            _ticketAssignmentHistoryRepository = ticketAssignmentHistoryRepository;
            _ticketCommentRepository = ticketCommentRepository;
        }

        public async Task<TicketDetailDTO> ComplateTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto)
        {
            var now = DateTime.UtcNow;
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket bulunamadı");

            if (ticket.RequesterId != userId)
                throw new UnauthorizedAccessException("Sadece kendi ticket'ınızı tamamlayabilirsiniz.");

            var statuses = await _lookupRepository.GetLookupsByTypeCodeAsync("TicketStatus", null, 200);
            var resolved = statuses.FirstOrDefault(x => x.Code == "Resolved");

            if (resolved == null) throw new Exception("Resolved statüsü bulunamadı");

            if (ticket.StatusId == resolved.Id)
                throw new Exception("Ticket zaten tamamlanmış");

            var fromStatusId = ticket.StatusId;

            ticket.StatusId = dto.StatusId;
            ticket.ResolvedAt ??= now;
            ticket.UpdatedAt = now;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            if (ticket.StatusId == 13UL)
            {
                ticket.ClosedAt = now;
                _ticketRepository.Update(ticket);
                await _ticketRepository.SaveChangesAsync();
            }

            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                EventType = "StatusChanged",
                ActorId = userId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "StatusChanged",
                    fromStatusId,
                    toStatusId = ticket.StatusId
                }),
                CreatedAt = now
            });
            await _ticketEventRepository.SaveChangesAsync();

            return _mapper.Map<TicketDetailDTO>(ticket);
        }

        public async Task<TicketDetailDTO> CreateTicketAsync(CreateTicketDTO dto, ulong requesterId)
        {
            var requesterForId = dto.RequestedForId ?? requesterId;

            var requestedForUser = await _userRepository.GetByIdAsync(requesterForId);
            if (requestedForUser == null)
                throw new Exception("User Bulunamadı");
            if (dto.ConfigurationItemId.HasValue)
            {
                var ok = await _configurationItemsRepository.IsConfigurationItemOwnedByUserAsync(dto.ConfigurationItemId.Value, requesterForId);
                if (!ok) throw new Exception("Configuration Item bu kullanıcıya ait değil / bulunamadı.");
            }
            ulong? assignedTeamId = null;

            if (dto.SubcategoryId.HasValue)
            {
                var subcategory = await _ticketCategoryRepository.GetByIdAsync(dto.SubcategoryId.Value);
                assignedTeamId = subcategory?.DefaultTeamId;
            }
            if (!assignedTeamId.HasValue && dto.CategoryId.HasValue)
            {
                var category = await _ticketCategoryRepository.GetByIdAsync(dto.CategoryId.Value);
                assignedTeamId = category?.DefaultTeamId;
            }

            const ulong StatusNewId = 5;

            var ticket = new Ticket
            {
                TicketNumber = string.Empty,

                TypeId = dto.TypeId,
                StatusId = StatusNewId,

                RequesterId = requesterId,
                RequestedForId = requesterForId,

                DepartmentId = requestedForUser.DepartmentId,
                LocationId = requestedForUser.LocationId,

                Title = dto.Title,
                Description = dto.Description,
                DueAt = dto.DueAt,

                CategoryId = dto.CategoryId,
                SubcategoryId = dto.SubcategoryId,

                PriorityId = dto.PriorityId,
                ImpactId = null,
                UrgencyId = null,

                ServiceId = dto.ServiceId,
                ConfigurationItemId = dto.ConfigurationItemId,

                AssignedTeamId = assignedTeamId,
                AssigneeId = null,
                IsMajor = false,
                ReopenedCount = 0,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            var ticketNumber = await _ticketRepository.GenerateTicketNumberAsync(ticket.Id);
            ticket.TicketNumber = ticketNumber;
            await _ticketRepository.SaveChangesAsync();

            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                EventType = "TicketCreated",
                ActorId = requesterId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "TicketCreated",
                    ticketNumber = ticket.TicketNumber,
                    typeId = ticket.TypeId,
                    priorityId = ticket.PriorityId,
                    assignedTeamId = ticket.AssignedTeamId
                }),
                CreatedAt = DateTime.UtcNow
            });
            await _ticketEventRepository.SaveChangesAsync();
            var created = await _ticketRepository.GetByTicketIdAsync(ticket.Id);
            return _mapper.Map<TicketDetailDTO>(created);
        }

        public async Task<TicketDetailDTO?> GetMyTicketByIdAsync(ulong userId, ulong ticketId)
        {
            var entity = await _ticketRepository.GetMyTicketByIdAsync(userId, ticketId);
            return _mapper.Map<TicketDetailDTO>(entity);
        }

        public async Task<PagedResultDTO<TicketMiniDTO>> GetMyTicketsPageAsync(ulong userId, int pageNumber, int pageSize)
        {
            var entity = await _ticketRepository.GetMyTicketsPageAsync(userId, pageNumber, pageSize);

            return new PagedResultDTO<TicketMiniDTO>
            {
                TotalCount = entity.TotalCount,
                Page = entity.Page,
                PageSize = entity.PageSize,
                Items = _mapper.Map<List<TicketMiniDTO>>(entity.Items)
            };
        }

        public async Task<TicketDetailDTO> GetTicketByIdAsync(ulong id)
        {
            var entity = await _ticketRepository.GetByTicketIdAsync(id);
            return _mapper.Map<TicketDetailDTO>(entity);

        }

        public async Task<PagedResultDTO<TicketMiniDTO>> GetTicketsPageAsync(int pageNumber, int pageSize)
        {
            var entity = await _ticketRepository.GetTicketsPageAsync(pageNumber, pageSize);

            return new PagedResultDTO<TicketMiniDTO>
            {
                TotalCount = entity.TotalCount,
                Page = entity.Page,
                PageSize = entity.PageSize,
                Items = _mapper.Map<List<TicketMiniDTO>>(entity.Items)
            };
        }

        public async Task<TicketDetailDTO> ReopenTicketByIdAsync(ulong ticketId, ulong userId)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket bulunamadı");

            if (ticket.RequesterId != userId)
                throw new UnauthorizedAccessException("Sadece kendi ticket'ınızı yeniden açabilirsiniz.");

            var statuses = await _lookupRepository.GetLookupsByTypeCodeAsync("TicketStatus", null, 200);

            var closed = statuses.FirstOrDefault(x => x.Code == "Resolved");
            if (closed == null) throw new Exception("Closed statüsü bulunamadı");

            if (ticket.StatusId != closed.Id)
                throw new Exception("Sadece kapatılmış ticket'ları yeniden açabilirsiniz.");

            var reopened = statuses.FirstOrDefault(x => x.Code == "Reopened");
            if (reopened == null) throw new Exception("Reopened statüsü bulunamadı");

            var fromStatusId = ticket.StatusId;

            ticket.StatusId = reopened.Id; 
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.ClosedAt = null;
            ticket.ResolvedAt = null;
            ticket.ReopenedCount += 1;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                EventType = "StatusChanged",
                ActorId = userId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "StatusChanged",
                    fromStatusId,
                    toStatusId = reopened.Id,
                    reopenedCount = ticket.ReopenedCount
                }),
                CreatedAt = DateTime.UtcNow
            });
            await _ticketEventRepository.SaveChangesAsync();

            var updated = await _ticketRepository.GetByTicketIdAsync(ticketId);
            return _mapper.Map<TicketDetailDTO>(updated);
        }

        public async Task<TicketDetailDTO> UpdateTicketAssignment(ulong ticketId, UpdateTicketAssignmentDTO dto, ulong adminId)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket bulunamadı");

            if (!dto.AssigneeId.HasValue)
                throw new Exception("AssigneeId zorunlu.");

            var assignee = await _userRepository.GetByIdAsync(dto.AssigneeId.Value);
            if (assignee == null) throw new Exception("Assignee bulunamadı");

            if (!assignee.TeamId.HasValue)
                throw new Exception("Assignee'nin bir takımı yok. Önce team atayın.");
            var oldAssigneeId = ticket.Assignee.Id;
            var oldTeamId = ticket.Assignee.Team.Id;
            ticket.AssigneeId = assignee.Id;
            ticket.AssignedTeamId = assignee.TeamId.Value;

            ticket.UpdatedAt = DateTime.UtcNow;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();
            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                EventType = "Ticket Güncellendi",
                ActorId = ticket.AssigneeId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "Ticket Updated",

                }),
                CreatedAt = DateTime.UtcNow
            });
            await _ticketEventRepository.SaveChangesAsync();
            await _ticketAssignmentHistoryRepository.AddAsync(new TicketAssignmentHistory
            {
                TicketId = ticket.Id,
                NewAssigneeId = assignee.Id,
                OldAssigneeId = oldAssigneeId,
                NewTeamId = assignee.TeamId.Value,
                OldTeamId = oldTeamId,
                ChangeTypeId = ticket.Status.Id,
                ChangedById = ticket.Assignee.Id,
                ChangedAt = DateTime.UtcNow
            });
            await _ticketAssignmentHistoryRepository.SaveChangesAsync();
            var updated = await _ticketRepository.GetByTicketIdAsync(ticketId);
            return _mapper.Map<TicketDetailDTO>(updated);
        }

        public async Task<TicketDetailDTO> UpdateTicketStatusAsync(ulong ticketId, UpdateStatuTicketDTO dto, ulong userId)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) { throw new Exception("Ticket bulunamadı"); }
            if (dto == null)
            {
                throw new Exception("Güncelleme bilgileri sağlanmadı");
            }
            var fromStatusId = ticket.StatusId;
            ticket.StatusId = dto.StatusId;
            ticket.UpdatedAt = DateTime.UtcNow;
            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                EventType = "StatusChanged",
                ActorId = userId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "StatusChanged",
                    fromStatusId,
                    toStatusId = dto.StatusId
                }),
                CreatedAt = DateTime.UtcNow
            });
            await _ticketEventRepository.SaveChangesAsync();
            var updated = await _ticketRepository.GetByTicketIdAsync(ticketId);
            return _mapper.Map<TicketDetailDTO>(updated);
        }

        public async Task<TicketDetailDTO> UpdateTicketResolveAsync(ulong ticketId, TicketResolveDTO dto, ulong userId)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket bulunamadı");

            var statuses = await _lookupRepository.GetLookupsByTypeCodeAsync("TicketStatus", null, 200);

            var resolvedStatus = statuses.FirstOrDefault(x => x.Code == "Resolved");
            if (resolvedStatus == null) throw new Exception("Resolved statüsü bulunamadı");

            if (ticket.StatusId == resolvedStatus.Id)
                throw new Exception("Ticket zaten resolved durumda.");

            var closedStatus = statuses.FirstOrDefault(x => x.Code == "Closed");
            if (closedStatus != null && ticket.StatusId == closedStatus.Id)
                throw new Exception("Closed ticket resolve edilemez.");

            var fromStatusId = ticket.StatusId;

            var now = DateTime.UtcNow;
            ticket.StatusId = resolvedStatus.Id;
            ticket.ResolvedAt = now;
            ticket.ClosedAt = null;
            ticket.UpdatedAt = now;

            if (!string.IsNullOrWhiteSpace(dto.RootCause))
                ticket.RootCause = dto.RootCause;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();
            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                ActorId = userId,
                EventType = "StatusChanged",
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "StatusChanged",
                    fromStatusId,
                    toStatusId = resolvedStatus.Id
                }),
                CreatedAt = now
            });
            await _ticketEventRepository.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(dto.ResolutionComment))
            {
                var visibilityId = dto.IsPublicComment ? 37UL : 38UL;

                await _ticketCommentRepository.AddAsync(new TicketComment
                {
                    TicketId = ticket.Id,
                    AuthorId = userId,
                    Body = dto.ResolutionComment,
                    VisibilityId = visibilityId,
                    CreatedAt = now
                });
                await _ticketCommentRepository.SaveChangesAsync();

                await _ticketEventRepository.AddAsync(new TicketEvent
                {
                    TicketId = ticket.Id,
                    ActorId = userId,
                    EventType = "CommentAdded",
                    PayloadJson = JsonSerializer.Serialize(new
                    {
                        message = "CommentAdded",
                        isPublic = dto.IsPublicComment,
                        kind = "Resolution"
                    }),
                    CreatedAt = now
                });
                await _ticketEventRepository.SaveChangesAsync();
            }
            return _mapper.Map<TicketDetailDTO>(ticket);
        }

        public async Task<TicketDetailDTO> ClosedTicketByIdAsync(ulong ticketId, ulong userId, string? comment)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket bulunamadı");

            var statuses = await _lookupRepository
                .GetLookupsByTypeCodeAsync("TicketStatus", null, 200);

            var resolved = statuses.FirstOrDefault(x => x.Code == "Resolved");
            var closed = statuses.FirstOrDefault(x => x.Code == "Closed");

            if (resolved == null || closed == null)
                throw new Exception("Gerekli statüler bulunamadı");

            if (ticket.StatusId != resolved.Id)
                throw new Exception("Sadece Resolved durumundaki ticket kapatılabilir");

            var now = DateTime.UtcNow;

            var fromStatusId = ticket.StatusId;

            ticket.StatusId = closed.Id;
            ticket.ClosedAt = now;
            ticket.UpdatedAt = now;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(comment))
            {
                await _ticketCommentRepository.AddAsync(new TicketComment
                {
                    TicketId = ticket.Id,
                    AuthorId = userId,
                    Body = comment,
                    VisibilityId = 37, // Public
                    CreatedAt = now
                });
                await _ticketCommentRepository.SaveChangesAsync();
            }

            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticket.Id,
                ActorId = userId,
                EventType = "StatusChanged",
                PayloadJson = JsonSerializer.Serialize(new
                {
                    fromStatusId,
                    toStatusId = closed.Id,
                    message = "TicketClosed"
                }),
                CreatedAt = now
            });

            await _ticketEventRepository.SaveChangesAsync();

            return _mapper.Map<TicketDetailDTO>(ticket);
        }
    }
}