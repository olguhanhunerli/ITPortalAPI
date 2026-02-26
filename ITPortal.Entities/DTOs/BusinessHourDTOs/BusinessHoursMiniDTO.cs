using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.BusinessHourDTOs
{
    public class BusinessHoursMiniDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = null!;
        public string TimeZone { get; set; } = null!;
        public bool Is24x7 { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
