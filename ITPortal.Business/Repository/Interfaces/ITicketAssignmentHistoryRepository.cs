using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ITicketAssignmentHistoryRepository : IGenericRepository<TicketAssignmentHistory, ulong>
    {
        Task<PagedResultDTO<TicketAssignmentHistory>> GetAllTicketAssignmentHistoryAsync(int pageNumber, int pageSize);
        Task<List<TicketAssignmentHistory>> GetTicketAssignmentHistoryByIdAsync(ulong ticketId);
    }
}
