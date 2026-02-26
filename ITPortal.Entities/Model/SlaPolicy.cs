using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class SlaPolicy : AuditableEntity<ulong>
    {
        public string Name { get; set; } = null!;

        public ulong? AppliesToTypeId { get; set; }

        public ulong PriorityId { get; set; }

        public int FirstResponseMinutes { get; set; }

        public int ResolutionMinutes { get; set; }

        public ulong? BusinessHoursId { get; set; }

        public bool IsActive { get; set; } = true;



        public virtual Lookup? AppliesToType { get; set; }

        public virtual Lookup Priority { get; set; } = null!;

        public virtual BusinessHours? BusinessHours { get; set; }
        public ICollection<TicketSlaRun> TicketSlaRuns { get; set; } = new List<TicketSlaRun>();
    }
}
