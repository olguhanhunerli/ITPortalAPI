using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserDTOs
{
    public class CreateUserDTO
    {

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Username { get; set; }

        public ulong? DepartmentId { get; set; }
        public ulong? TeamId { get; set; }
        public ulong? LocationId { get; set; }

        public string? Title { get; set; }

        public string? Phone { get; set; }


        public string? Password { get; set; }

    }
}
