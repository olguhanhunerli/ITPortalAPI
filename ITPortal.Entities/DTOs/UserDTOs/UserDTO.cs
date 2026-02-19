using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserDTOs
{
    public class UserDTO
    {
        public ulong Id { get; set; }

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;
        [StringLength(80)]
        public string? Username { get; set; }

        public ulong? DepartmentId { get; set; }
        public ulong? TeamId { get; set; }
        public ulong? LocationId { get; set; }

        public string? Title { get; set; }

        public string? Phone { get; set; }


        public string? ExternalProvider { get; set; }

        public string? ExternalId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DepartmentName { get; set; }
        public string? TeamName { get; set; }
        public string? LocationName { get; set; }
    }
}
