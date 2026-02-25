using ITPortal.Entities.DTOs.TicketEventDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketEventService
    {
        Task<PagedResultDTO<TicketEventDTO>>GetTicketEvent(int page, int pageSize);
    }
}
