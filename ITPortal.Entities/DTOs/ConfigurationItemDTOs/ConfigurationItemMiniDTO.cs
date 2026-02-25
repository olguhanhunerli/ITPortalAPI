using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.ConfigurationItemDTOs
{
    public class ConfigurationItemMiniDTO
    {
        public string CiTypeName { get; set; } = null!;

        public string CiStatusName { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string NameTr { get; set; } = null!;
        public string? SerialNumber { get; set; }
        public string? AssetTag { get; set; }

        public string? OwnerUserName { get; set; }

        public string? OwnerTeamName { get; set; }

        public string? LocationName { get; set; }



    }
}
