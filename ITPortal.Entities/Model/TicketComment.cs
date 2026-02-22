using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class TicketComment : AuditableEntity<TicketComment>
    {
        public ulong TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public ulong AuthorId { get; set; }
        public User Author { get; set; }
        public ulong VisibilityId { get; set; }
        public Lookup Visibility { get; set; }
        public string Body { get; set; }
    }
}
