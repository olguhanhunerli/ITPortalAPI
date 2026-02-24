using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketDTOs
{
    public class UpdateTicketSlaDTO
    {
        public ulong? PriorityId { get; set; }
        public ulong? ImpactId { get; set; }
        public ulong? UrgencyId { get; set; }
        public DateTime? DueAt { get; set; }
    }
}
