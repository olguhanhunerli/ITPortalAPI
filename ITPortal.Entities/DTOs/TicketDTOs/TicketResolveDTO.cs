using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketDTOs
{
    public class TicketResolveDTO
    {
        public string? RootCause { get; set; }
        public string? ResolutionComment { get; set; }
        public bool IsPublicComment { get; set; } = true;
    }
}
