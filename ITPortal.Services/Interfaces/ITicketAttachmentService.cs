using ITPortal.Entities.DTOs.TicketAttachmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketAttachmentService
    {
        Task<TicketAttachmentMiniDTO> GetTicketAttachmentMini(ulong ticketId);
        Task<TicketAttachmentMiniDTO> CreateTicketAttachmentAsyn(ulong ticketId, CreateTicketAttachmentDTO dto, ulong uploadedBy, CancellationToken ct = default);
        Task<(byte[] Content, string ContentType, string FileName)> DownloadTicketAttachmentAsync(ulong ticketId, ulong attachmentId);
        Task<(byte[] Content, string ContentType, string FileName)> DownloadTicketAttachmentForUserAsync(ulong ticketId, ulong attachmentId, ulong currentUserId);
    }
}
