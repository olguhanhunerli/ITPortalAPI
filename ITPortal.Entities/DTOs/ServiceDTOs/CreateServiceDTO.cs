using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ServiceDTOs
{
    public class CreateServiceDTO
    {
        public string Name { get; set; } = null!;

        public string? NameTr { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTr { get; set; }

        public ulong OwnerTeamId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
