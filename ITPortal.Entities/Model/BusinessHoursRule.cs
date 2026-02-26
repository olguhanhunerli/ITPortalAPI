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

        public BusinessDay DayOfWeek { get; set; }  

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
        public BusinessHours BusinessHours { get; set; } = null!;
    }
    public enum BusinessDay : byte
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }
}
