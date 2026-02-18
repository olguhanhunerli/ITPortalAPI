using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class User
    {
        [Key]
        [Column("Id")]
        public ulong Id { get; set; } 

        [Required, StringLength(160)]
        public string FullName { get; set; } = default!;

        [Required, StringLength(190)]
        public string Email { get; set; } = default!;

        [StringLength(80)]
        public string? Username { get; set; }

        public ulong? DepartmentId { get; set; }
        public ulong? TeamId { get; set; }
        public ulong? LocationId { get; set; }

        [StringLength(120)]
        public string? Title { get; set; }

        [StringLength(40)]
        public string? Phone { get; set; }

        [Required, StringLength(20)]
        public string AuthType { get; set; } = "SSO";

        [StringLength(255)]
        public string? PasswordHash { get; set; }

        [StringLength(40)]
        public string? ExternalProvider { get; set; }

        [StringLength(190)]
        public string? ExternalId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public Team? Team { get; set; }
        public Location? Location { get; set; }
    }
}
