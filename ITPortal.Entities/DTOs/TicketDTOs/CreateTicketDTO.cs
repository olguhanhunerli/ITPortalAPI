using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketDTOs
{
    public class CreateTicketDTO
    {
        public ulong TypeId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ulong? CategoryId { get; set; }
        public ulong? SubcategoryId { get; set; }
        public ulong? PriorityId { get; set; }
        public ulong? ServiceId { get; set; }
        public ulong? ConfigurationItemId { get; set; }
        public ulong? RequestedForId { get; set; }
        public DateTime? DueAt { get; set; }
    }
}
