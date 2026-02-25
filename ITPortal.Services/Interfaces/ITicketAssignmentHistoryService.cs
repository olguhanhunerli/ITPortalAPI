using ITPortal.Entities.DTOs.TicketAssignmentHistoryDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketAssignmentHistoryService
    {
        Task<PagedResultDTO<TicketAssignmentHistoryDetailDTO>> GetTicketAssignmentHistoryAllAsync(int pageNumber, int pageSize);
        Task<List<TicketAssignmentHistoryDetailDTO>> GetTicketAssignmentHistoryByTicketIdAsync(ulong ticketId);
    }
}
