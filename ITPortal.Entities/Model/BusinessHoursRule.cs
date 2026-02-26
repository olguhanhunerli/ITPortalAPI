using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class BusinessHoursRule
    {
        public ulong Id { get; set; }

        public ulong BusinessHoursId { get; set; }

        public byte DayOfWeek { get; set; }  

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public BusinessHours BusinessHours { get; set; } = null!;
    }
}
