using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserDTOs
{
    public class UserMiniDTO
    {
        public ulong Id { get; set; }

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Username { get; set; }

        public bool IsActive { get; set; }

        public string? DepartmentName { get; set; }

        public string? TeamName { get; set; }
        public List<string> Roles { get; set; } = new();

        public string? LocationName { get; set; }
    }
}
