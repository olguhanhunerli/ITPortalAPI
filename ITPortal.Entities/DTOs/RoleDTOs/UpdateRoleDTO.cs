using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.RoleDTOs
{
    public class UpdateRoleDTO
    {
        public string Name { get; set; } = default!;
        public string NameTr { get; set; }
        public string DescriptionTr { get; set; }
        public string? Description { get; set; }
    }
}
