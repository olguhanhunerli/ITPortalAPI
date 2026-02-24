using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;

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
        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IUserRepository userRepository, ITicketCategoryRepository ticketCategoryRepository, ILookupRepository lookupRepository, ITeamRepository teamRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
            _lookupRepository = lookupRepository;
            _teamRepository = teamRepository;
        }

        public async Task<TicketDetailDTO> ComplateTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket bulunamadı");

            if (ticket.RequesterId != userId)
                throw new UnauthorizedAccessException("Sadece kendi ticket'ınızı tamamlayabilirsiniz.");

            var statuses = await _lookupRepository.GetLookupsByTypeCodeAsync("TicketStatus", null, 200);
            var resolved = statuses.FirstOrDefault(x => x.Code == "Resolved");

            if (resolved == null) throw new Exception("Resolved statüsü bulunamadı");

            if (ticket.StatusId == resolved.Id)
                throw new Exception("Ticket zaten tamamlanmış");

            var now = DateTime.UtcNow;
          
            ticket.StatusId = dto.StatusId;
            ticket.ResolvedAt ??= now;
            ticket.UpdatedAt = now;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            var updatedTicket = await _ticketRepository.GetByTicketIdAsync(ticketId);

            if (updatedTicket.StatusId == 13)
                ticket.ClosedAt = DateTime.UtcNow;
            _ticketRepository.Update(updatedTicket);
            await _ticketRepository.SaveChangesAsync();
            return _mapper.Map<TicketDetailDTO>(updatedTicket);
        }

        public async Task<TicketDetailDTO> CreateTicketAsync(CreateTicketDTO dto, ulong requesterId)
        {
            var requesterForId = dto.RequestedForId ?? requesterId;

            var requestedForUser = await _userRepository.GetByIdAsync(requesterForId);
            if (requestedForUser == null)
                throw new Exception("User Bulunamadı");

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

            var created = await _ticketRepository.GetByTicketIdAsync(ticket.Id);
            return _mapper.Map<TicketDetailDTO>(created);
        }

        public async Task<TicketDetailDTO?> GetMyTicketByIdAsync(ulong userId, ulong ticketId)
        {
            var entity = await _ticketRepository.GetMyTicketByIdAsync(userId,ticketId);
            return _mapper.Map<TicketDetailDTO>(entity);
        }

        public async Task<PagedResultDTO<TicketMiniDTO>> GetMyTicketsPageAsync(ulong userId, int pageNumber, int pageSize)
        {
            var entity = await _ticketRepository.GetMyTicketsPageAsync(userId,pageNumber, pageSize);

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

        public async Task<TicketDetailDTO> ReopenTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto)
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

            ticket.StatusId = dto.StatusId;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.ClosedAt = null;
            ticket.ResolvedAt = null;
            ticket.ReopenedCount += 1;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

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

            ticket.AssigneeId = assignee.Id;
            ticket.AssignedTeamId = assignee.TeamId.Value;

            ticket.UpdatedAt = DateTime.UtcNow;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            var updated = await _ticketRepository.GetByTicketIdAsync(ticketId);
            return _mapper.Map<TicketDetailDTO>(updated);
        }
    }
}
