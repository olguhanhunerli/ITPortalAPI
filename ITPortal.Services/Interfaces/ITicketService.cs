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
        //Admin and agents can see all tickets, but requesters can only see their own tickets. So we need to check the role of the user before returning the tickets.
        Task<PagedResultDTO<TicketMiniDTO>> GetTicketsPageAsync(int pageNumber, int pageSize);
        Task<TicketDetailDTO> GetTicketByIdAsync(ulong id);
        Task<TicketDetailDTO> UpdateTicketAssignment(ulong ticketId, UpdateTicketAssignmentDTO dto, ulong adminId);

        //Requester And Admin can create tickets, but agents cannot create tickets. So we need to check the role of the user before allowing them to create a ticket.
        Task<TicketDetailDTO> CreateTicketAsync(CreateTicketDTO dto, ulong requesterId);
        Task<TicketDetailDTO> UpdateTicketStatusAsync(ulong ticketId, UpdateStatuTicketDTO dto, ulong userId);
        Task<TicketDetailDTO> UpdateTicketResolveAsync(ulong ticketId, TicketResolveDTO dto , ulong userId);
        Task<TicketDetailDTO> ClosedTicketByIdAsync(ulong ticketId, ulong userId, string? comment);

        //Requester can only see their own tickets, but agents and admins can see all tickets. So we need to check the role of the user before returning the tickets.
        Task<PagedResultDTO<TicketMiniDTO>> GetMyTicketsPageAsync(ulong userId, int pageNumber, int pageSize);
        Task<TicketDetailDTO?> GetMyTicketByIdAsync(ulong userId, ulong ticketId);
        Task<TicketDetailDTO> ComplateTicketByIdAsync(ulong ticketId, ulong userId, UpdateStatuTicketDTO dto);
        Task<TicketDetailDTO> ReopenTicketByIdAsync(ulong ticketId, ulong userId);
    }
}
