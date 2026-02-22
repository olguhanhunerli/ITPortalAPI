using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketCategoryDTOs
{
    public class CreateTicketCategoryDTO
    {
        public string Code { get; set; } = null!;
        public string? NameTr { get; set; }
        public string? NameEn { get; set; }
        public string? DescriptionTr { get; set; }
        public string? DescriptionEn { get; set; }
        public ulong? ParentId { get; set; }
        public ulong? DefaultTeamId { get; set; }

        public bool RequiresApproval { get; set; }
        public ulong? ApprovalTypeId { get; set; }

        public string? FormSchemaJson { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
