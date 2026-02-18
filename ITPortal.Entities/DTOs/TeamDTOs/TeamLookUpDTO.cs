using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TeamDTOs
{
    public class TeamLookUpDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
