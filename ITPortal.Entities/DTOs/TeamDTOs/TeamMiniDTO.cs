using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TeamDTOs
{
    public class TeamMiniDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = default!;
        public ulong? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int UserCount { get; set; }
    }
}
