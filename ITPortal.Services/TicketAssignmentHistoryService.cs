using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketAssignmentHistoryDTOs;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;

namespace ITPortal.Services
{
    public class TicketAssignmentHistoryService : ITicketAssignmentHistoryService
    {
        private readonly ITicketAssignmentHistoryRepository _ticketAssignmentHistoryService;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        public TicketAssignmentHistoryService(IMapper mapper, ITicketAssignmentHistoryRepository ticketAssignmentHistoryService, ITicketRepository ticketRepository)
        {

            _mapper = mapper;
            _ticketAssignmentHistoryService = ticketAssignmentHistoryService;
            _ticketRepository = ticketRepository;
        }

        public async Task<PagedResultDTO<TicketAssignmentHistoryDetailDTO>> GetTicketAssignmentHistoryAllAsync(int pageNumber, int pageSize)
        {
            var entity = await _ticketAssignmentHistoryService.GetAllTicketAssignmentHistoryAsync(pageNumber, pageSize);
            var mappedEvents = _mapper.Map<List<TicketAssignmentHistoryDetailDTO>>(entity.Items);

            return new PagedResultDTO<TicketAssignmentHistoryDetailDTO>
            {
                TotalCount = entity.TotalCount,
                Page = entity.Page,
                PageSize = entity.PageSize,
                Items = mappedEvents
            };
        }

        public async Task<List<TicketAssignmentHistoryDetailDTO>> GetTicketAssignmentHistoryByTicketIdAsync(ulong ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket Bulunamadı");
            var events = await _ticketAssignmentHistoryService.GetTicketAssignmentHistoryByIdAsync(ticket.Id);
            if (events == null) throw new Exception("Tickete Ait History Bulunamadı.");
            return _mapper.Map<List<TicketAssignmentHistoryDetailDTO>>(events);
        }
    }
}
