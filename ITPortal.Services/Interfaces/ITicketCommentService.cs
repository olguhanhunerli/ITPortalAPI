using ITPortal.Entities.DTOs.TicketCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketCommentService
    {
        Task<TicketCommentDTO> CreateTicketCommentAsync(ulong ticketId,CreateCommentDTO createTicketCommentDTO, ulong userId);
    }
}
