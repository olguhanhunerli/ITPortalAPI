using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketAttachmentDTOs;
using ITPortal.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository
{
    public class TicketAttachmentRepository : GenericRepository<TicketAttachment, ulong>, ITicketAttachmentRepository
    {
        public TicketAttachmentRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<List<TicketAttachmentMiniDTO>> GetAttachmentsByTicketIdAsync(ulong ticketId)
        {
            return await _set
                .Where(a => a.TicketId == ticketId)
                .Select(a => new TicketAttachmentMiniDTO
                {
                    Id = a.Id,
                   ContentType = a.ContentType,
                   CreatedAt = a.CreatedAt,
                   FileName = a.FileName,
                   FileSizeBytes = (long)a.FileSizeBytes
                })
                .ToListAsync();
        }
    }
}
