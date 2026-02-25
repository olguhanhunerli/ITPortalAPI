using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.TicketAttachmentDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ITicketAttachmentRepository : IGenericRepository<TicketAttachment, ulong>
    {
        Task<List<TicketAttachmentMiniDTO>> GetAttachmentsByTicketIdAsync(ulong ticketId);
    }
}
