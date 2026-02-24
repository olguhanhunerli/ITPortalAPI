using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class Ticket : AuditableEntity<ulong>
    {
        public string TicketNumber { get; set; } = null!;
        public ulong TypeId { get; set; }
        public ulong StatusId { get; set; }
        public ulong? PriorityId { get; set; }
        public ulong? ImpactId { get; set; }
        public ulong? UrgencyId { get; set; }

        public ulong? CategoryId { get; set; }
        public ulong? SubcategoryId { get; set; }

        public ulong? ServiceId { get; set; }
        public Service Service { get; set; }
        public ulong? ConfigurationItemId { get; set; }
        public ConfigurationItem? ConfigurationItem { get; set; }

        public ulong RequesterId { get; set; }
        public ulong? RequestedForId { get; set; }
        public ulong? AssigneeId { get; set; }
        public ulong? AssignedTeamId { get; set; }

        public ulong? LocationId { get; set; }
        public ulong? DepartmentId { get; set; }

        public bool IsMajor { get; set; }
        public ulong? ApprovalStateId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string? DetailsJson { get; set; }
        public DateTime? DueAt { get; set; }
        public string? RootCause { get; set; }
        public int ReopenedCount { get; set; }

        public DateTime? FirstResponseDueAt { get; set; }
        public DateTime? ResolutionDueAt { get; set; }
        public DateTime? FirstRespondedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public TicketCategory? Category { get; set; }
        public TicketCategory? Subcategory { get; set; }

        public User Requester { get; set; } = null!;
        public User? RequestedFor { get; set; }
        public User? Assignee { get; set; }
        public Team? AssignedTeam { get; set; }

        public Department? Department { get; set; }
        public Location? Location { get; set; }

        public Lookup Type { get; set; } = null!;
        public Lookup Status { get; set; } = null!;
        public Lookup? Priority { get; set; }
        public Lookup? Impact { get; set; }
        public Lookup? Urgency { get; set; }
        public Lookup? ApprovalState { get; set; }
        public MajorIncident? MajorIncident { get; set; }

        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
        public ICollection<TicketEvent> Events { get; set; } = new List<TicketEvent>();
    }
}
