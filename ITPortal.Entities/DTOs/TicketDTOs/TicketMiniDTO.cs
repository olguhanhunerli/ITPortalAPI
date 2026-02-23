using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketDTOs
{
    public class TicketMiniDTO
    {
        public ulong Id { get; set; }
        public string TicketNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ulong StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public ulong? PriorityId { get; set; }
        public string? PriorityName { get; set; }

        public ulong RequesterId { get; set; }
        public string RequesterName { get; set; } = null!;

        public ulong? AssigneeId { get; set; }
        public string? AssigneeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueAt { get; set; }
        public bool IsMajor { get; set; }
    }
}
