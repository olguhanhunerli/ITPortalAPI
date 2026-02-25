using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketAssignmentHistoryDTOs
{
    public class TicketAssignmentHistoryDetailDTO
    {
        public ulong Id { get; set; }
        public ulong TicketId { get; set; }
        public string TicketNumber { get; set; }

        public ulong ChangeTypeId { get; set; }
        public string ChangeTypeName { get; set; }

        public ulong OldAssigneeId { get; set; }
        public string OldAssigneeName { get; set; }

        public ulong NewAssigneeId { get; set; }
        public string NewAssigneeName { get; set; }

        public ulong OldTeamId { get; set; }
        public string OldTeamName { get; set; }

        public ulong NewTeamId { get; set; }
        public string NewTeamName { get; set; }

        public ulong ChangedById { get; set; }
        public string ChangedByName { get; set; }

        public DateTime ChangeAt { get; set; }
    }
}
