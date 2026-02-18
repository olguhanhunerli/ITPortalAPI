using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TeamDTOs
{
    public class TeamDTO
    {
        public ulong Id { get; set; }

        public ulong? DepartmentId { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public string? DepartmentName { get; set; }
    }
}
