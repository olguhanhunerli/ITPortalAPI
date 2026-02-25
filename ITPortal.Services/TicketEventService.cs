using AutoMapper;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.TicketEventDTOs;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketEventService : ITicketEventService
    {
        private readonly ITicketEventRepository _ticketEventRepository;
        private readonly IMapper _mapper;

        public TicketEventService(ITicketEventRepository ticketEventRepository, IMapper mapper)
        {
            _ticketEventRepository = ticketEventRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<TicketEventDTO>> GetTicketEvent(int page, int pageSize)
        {
            var pagedEvents = await _ticketEventRepository.GetTicketEventAsync(page, pageSize);
            var mappedEvents = _mapper.Map<List<TicketEventDTO>>(pagedEvents.Items);

            return new PagedResultDTO<TicketEventDTO>
            {
                TotalCount = pagedEvents.TotalCount,
                Page = pagedEvents.Page,
                PageSize = pagedEvents.PageSize,
                Items = mappedEvents
            };
        }
    }
}
