using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ServiceDTOs
{
    public class ServiceLookupDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NameTr { get; set; }
    }
}
