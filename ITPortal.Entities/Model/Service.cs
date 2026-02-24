using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class Service : AuditableEntity<ulong>
    {
        public string Name { get; set; }
        public string NameTr { get; set; }
        public string Description { get; set; }
        public string DescriptionTr { get; set; }
        public ulong OwnerTeamId { get; set; }
        public Team OwnerTeam { get; set; }
        public bool IsActive { get; set; }
    }
}
