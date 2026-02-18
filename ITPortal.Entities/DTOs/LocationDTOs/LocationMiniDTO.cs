using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.LocationDTOs
{
    public class LocationMiniDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = default!;
        public int UserCount { get; set; }
    }
}
