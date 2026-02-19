using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserRoleDTOs
{
    public class UserRoleDTO
    {
        public ulong RoleId { get; set; }
        public string RoleName { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
