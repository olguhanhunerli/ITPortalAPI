using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketCommentDTOs
{
    public class CreateCommentDTO
    {
        public ulong VisibilityId { get; set; }
        public string Body { get; set; }
    }
}
