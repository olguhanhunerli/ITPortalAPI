using ITPortal.Entities.Model.Common;

namespace ITPortal.Entities.Model
{
    public class TicketEvent : BaseEntity<TicketEvent>
    {
        public ulong TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public ulong? ActorId { get; set; }
        public User? Actor { get; set; }
        public string EventType { get; set; }
        public string? PayloadJson { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
