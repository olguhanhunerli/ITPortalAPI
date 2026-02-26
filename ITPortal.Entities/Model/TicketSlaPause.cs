using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class TicketSlaPause
    {
        public ulong Id { get; set; }

        public ulong TicketId { get; set; }

        public ulong ReasonId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public ulong? CreatedById { get; set; }

        public string? Note { get; set; }


        public Ticket Ticket { get; set; } = null!;

        public Lookup Reason { get; set; } = null!;

        public User? CreatedBy { get; set; }
    }
}
