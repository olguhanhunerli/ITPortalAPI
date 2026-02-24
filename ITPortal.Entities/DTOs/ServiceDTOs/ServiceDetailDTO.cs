using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ServiceDTOs
{
    public class ServiceDetailDTO
    {
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;
        public string? NameTr { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTr { get; set; }

        public ulong OwnerTeamId { get; set; }
        public string OwnerTeamName { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
