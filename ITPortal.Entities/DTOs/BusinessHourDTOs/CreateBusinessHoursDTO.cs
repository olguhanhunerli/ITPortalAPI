using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.BusinessHourDTOs
{
    public class CreateBusinessHoursDTO
    {
        public string Name { get; set; } = null!;
        public string TimeZone { get; set; } = "Europe/Istanbul";
        public bool Is24x7 { get; set; } = false;
    }
}
