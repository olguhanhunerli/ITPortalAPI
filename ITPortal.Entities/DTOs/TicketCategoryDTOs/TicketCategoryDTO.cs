using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketCategoryDTOs
{
    public class TicketCategoryDTO
    {
        public ulong Id { get; set; }
        public string Code { get; set; }
        public string NameTr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionTr { get; set; }
        public string DescriptionEn { get; set; }
        public ulong ParentId { get; set; }
        public string ParentName { get; set; }
        public ulong DefaultTeamId { get; set; } 
        public string DefaultTeamName { get; set; }
        public bool RequiresApproval { get; set; }
        public ulong ApprovalTypeId { get; set; }
        public string? FormSchemaJson { get; set; }
        public bool IsActive { get; set; }
    }
}
