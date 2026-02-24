using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketService
    {
        Task<PagedResultDTO<TicketMiniDTO>> GetTicketsPageAsync(int pageNumber, int pageSize);
        Task<TicketDetailDTO> GetTicketByIdAsync(ulong id);
        Task<TicketDetailDTO> CreateTicketAsync(CreateTicketDTO dto, ulong requesterId);
        Task<PagedResultDTO<TicketMiniDTO>> GetMyTicketsPageAsync(ulong userId, int pageNumber, int pageSize);
        Task<TicketDetailDTO?> GetMyTicketByIdAsync(ulong userId, ulong ticketId);
        Task<TicketDetailDTO> ComplateTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto);
        Task<TicketDetailDTO> ReopenTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto);
    }
}
