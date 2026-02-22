using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketCategoryDTOs
{
    public class TicketCategoryTreeDTO
    {
        public ulong Id { get; set; }
        public string Code { get; set; } = null!;
        public string? NameTr { get; set; }
        public string? NameEn { get; set; }
        public bool IsActive { get; set; }
        public List<TicketCategoryTreeDTO> Children { get; set; } = new();
    }
}
