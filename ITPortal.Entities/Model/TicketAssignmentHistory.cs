using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class TicketAssignmentHistory
    {
        public ulong Id { get; set; }
        public ulong TicketId { get; set; }
        public ulong ChangeTypeId { get; set; }
        public ulong OldAssigneeId { get; set; }
        public ulong NewAssigneeId { get; set; }
        public ulong OldTeamId { get; set; }
        public ulong NewTeamId { get; set; }
        public ulong ChangedById { get; set; }
        public DateTime ChangedAt { get; set; }
        public Ticket Ticket { get; set; }
        public Lookup ChangeType { get; set; }
        public User OldAssignee { get; set; }
        public User NewAssignee { get; set; }
        public User OldTeam { get; set; }
        public User NewTeam { get; set; }
        public User ChangedBy { get; set; }
    }
}
