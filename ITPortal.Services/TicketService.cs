using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketDetailDTO> GetTicketByIdAsync(ulong id)
        {
            var entity = await _ticketRepository.GetByIdAsync(id);
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
