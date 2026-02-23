using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class MajorIncident
    {
        public ulong Id { get; set; }

        public ulong TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public ulong StatusId { get; set; }
        public Lookup Status { get; set; } = null!;
        public DateTime DeclaredAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public ulong LeadId { get; set; }
        public User Lead { get; set; } = null!;

        public ICollection<MajorIncidentUpdate> Updates { get; set; } = new List<MajorIncidentUpdate>();
    }
}

