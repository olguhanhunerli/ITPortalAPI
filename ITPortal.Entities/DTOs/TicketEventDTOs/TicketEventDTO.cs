using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketEventDTOs
{
    public class TicketEventDTO
    {
        public ulong TicketId { get; set; }
        public string TicketNumber { get; set; }
        public ulong? ActorId { get; set; }
        public string? ActorName { get; set; }
        public string EventType { get; set; }
        public string? PayloadJson { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
