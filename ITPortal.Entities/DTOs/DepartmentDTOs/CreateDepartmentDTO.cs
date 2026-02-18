using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.DepartmentDTOs
{
    public class CreateDepartmentDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
