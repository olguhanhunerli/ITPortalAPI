using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ServiceDTOs
{
    public class UpdateServiceDTO
    {
        public string? Name { get; set; }
        public string? NameTr { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTr { get; set; }

        public ulong? OwnerTeamId { get; set; }
        public bool? IsActive { get; set; }
    }
}
