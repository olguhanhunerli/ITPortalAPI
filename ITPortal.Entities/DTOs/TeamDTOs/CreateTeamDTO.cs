using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TeamDTOs
{
    public class CreateTeamDTO
    {
        public string Name { get; set; } = default!;
        public ulong? DepartmentId { get; set; }
    }
}
