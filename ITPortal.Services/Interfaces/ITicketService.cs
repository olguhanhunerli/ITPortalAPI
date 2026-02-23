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
    }
}
