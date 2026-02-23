using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository; 
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IUserRepository userRepository, ITicketCategoryRepository ticketCategoryRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
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

            const ulong StatusNewId = 1;

            var ticket = new Ticket
            {
                TicketNumber = string.Empty,

                TypeId = dto.TypeId,
                StatusId = StatusNewId,

                RequesterId = requesterId,
                RequestedForId = dto.RequestedForId,

                DepartmentId = requestedForUser.DepartmentId,
                LocationId = requestedForUser.LocationId,

                Title = dto.Title,
                Description = dto.Description,
                DetailsJson = dto.DetailsJson,
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
    }
}
