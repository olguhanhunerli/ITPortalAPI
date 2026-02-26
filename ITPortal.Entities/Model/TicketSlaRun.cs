using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class TicketSlaRun
    {
        public ulong Id { get; set; }

        public ulong TicketId { get; set; }

        public ulong SlaPolicyId { get; set; }

        public DateTime? FirstResponseDueAt { get; set; }

        public DateTime? ResolutionDueAt { get; set; }

        public DateTime? FirstResponseBreachedAt { get; set; }

        public DateTime? ResolutionBreachedAt { get; set; }

        public DateTime CreatedAt { get; set; }


        // Navigation
        public virtual Ticket Ticket { get; set; } = null!;

        public virtual SlaPolicy SlaPolicy { get; set; } = null!;
    }
}
