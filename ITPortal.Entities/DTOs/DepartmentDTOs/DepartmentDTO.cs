using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.DepartmentDTOs
{
    public  class DepartmentDTO
    {
        public ulong Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

    }
}
