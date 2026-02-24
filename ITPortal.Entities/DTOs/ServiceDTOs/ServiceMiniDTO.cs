using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ServiceDTOs
{
    public class ServiceMiniDTO
    {
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;
        public string? NameTr { get; set; }

        public string OwnerTeamName { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
