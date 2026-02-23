using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketCommentDTOs
{
    public class TicketCommentDTO
    {
        public ulong TicketId { get; set; }
        public string TicketName { get; set; }
        public ulong AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ulong VisibilityId { get; set; }
        public string VisibilityName { get; set; }
        public string Body { get; set; }
    }
}
