using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ConfigurationItemDTOs
{
    public class CreateConfigurationItemDTO
    {
        public ulong CiTypeId { get; set; }
        public ulong CiStatusId { get; set; }

        public string Name { get; set; } = null!;
        public string NameTr { get; set; } = null!;
        public string? SerialNumber { get; set; }
        public string? AssetTag { get; set; }

        public ulong? OwnerUserId { get; set; }
        public ulong? OwnerTeamId { get; set; }
        public ulong? LocationId { get; set; }

        public string? DetailsJson { get; set; }

    }
}
