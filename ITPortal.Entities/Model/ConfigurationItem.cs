using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class ConfigurationItem : AuditableEntity<ulong>
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

        public Lookup CiType { get; set; } = null!;
        public Lookup CiStatus { get; set; } = null!;

        public User? OwnerUser { get; set; }
        public Team? OwnerTeam { get; set; }
        public Location? Location { get; set; }

    }
}
