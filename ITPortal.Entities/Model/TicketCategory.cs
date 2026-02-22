using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public  class TicketCategory : AuditableEntity<ulong>
    {
        public string Code { get; set; }
        public string NameTr { get; set; }
        public string NameEn { get; set; }
        public string? DescriptionTr { get; set; }
        public string? DescriptionEn { get; set; }
        public ulong? ParentId { get; set; }
        public ulong DefaultTeamId { get; set; }
        public bool RequiresApproval { get; set; }
        public ulong ApprovalTypeId { get; set; }
        public Lookup ApprovalType { get; set; }
        public string? FormSchemaJson { get; set; }
        public bool IsActive { get; set; }
        public TicketCategory Parent { get; set; }
        public Team DefaultTeam { get; set; }
        public ICollection<TicketCategory> Children { get; set; } = new List<TicketCategory>();
        public ICollection<Ticket> Tickets { get; set; }
    }
}
