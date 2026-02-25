using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketAttachmentDTOs
{
    public class CreateTicketAttachmentDTO
    {
        public IFormFile File { get; set; }
    }
}
