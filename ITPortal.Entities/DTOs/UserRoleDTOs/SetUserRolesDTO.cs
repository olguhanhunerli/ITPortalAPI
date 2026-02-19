using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserRoleDTOs
{
    public class SetUserRolesDTO
    {
        public List<ulong> RoleIds { get; set; } = new List<ulong>();
    }
}
