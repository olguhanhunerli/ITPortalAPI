using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class BusinessHours
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = null!;

        public string TimeZone { get; set; } = "Europe/Istanbul";

        public bool Is24x7 { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public ICollection<SlaPolicy>? SlaPolicies { get; set; }
        public ICollection<BusinessHoursRule> Rules { get; set; } = new List<BusinessHoursRule>();
    }
}
