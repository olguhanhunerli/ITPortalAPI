using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TeamDTOs
{
    public class UpdateTeamDTO
    {
        public ulong DepartmentId { get; set; }
        public string Name { get; set; }
    }
}
