using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class UserRole
    {
        public ulong UserId { get; set; }
        public User User { get; set; } = default!;

        public ulong RoleId { get; set; }
        public Role Role { get; set; } = default!;

        public DateTime AssignedAt { get; set; }

    }
}
