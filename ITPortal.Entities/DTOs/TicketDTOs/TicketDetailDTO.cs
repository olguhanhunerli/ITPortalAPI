using ITPortal.Entities.DTOs.MajorIncidentDTOs;
using ITPortal.Entities.DTOs.TicketCommentDTOs;
using ITPortal.Entities.DTOs.TicketEventDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.TicketDTOs
{
    public class TicketDetailDTO
    {
        public ulong Id { get; set; }
        public string TicketNumber { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? DetailsJson { get; set; }
        public string? RootCause { get; set; }
        public bool IsMajor { get; set; }
        public int ReopenedCount { get; set; }

        public ulong TypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public ulong StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public ulong? PriorityId { get; set; }
        public string? PriorityName { get; set; }

        public ulong? ImpactId { get; set; }
        public string? ImpactName { get; set; }

        public ulong? UrgencyId { get; set; }
        public string? UrgencyName { get; set; }

        public ulong? ApprovalStateId { get; set; }
        public string? ApprovalStateName { get; set; }

        public ulong? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ulong? SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; }

        public ulong? ServiceId { get; set; }
        public string? ServiceName { get; set; }

        public ulong? ConfigurationItemId { get; set; }
        public string? ConfigurationItemName { get; set; }

        public ulong RequesterId { get; set; }
        public string RequesterName { get; set; } = null!;

        public ulong? RequestedForId { get; set; }
        public string? RequestedForName { get; set; }

        public ulong? AssigneeId { get; set; }
        public string? AssigneeName { get; set; }

        public ulong? AssignedTeamId { get; set; }
        public string? AssignedTeamName { get; set; }

        public ulong? LocationId { get; set; }
        public string? LocationName { get; set; }

        public ulong? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public DateTime? DueAt { get; set; }
        public DateTime? FirstResponseDueAt { get; set; }
        public DateTime? ResolutionDueAt { get; set; }

        public DateTime? FirstRespondedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ulong? MajorIncidentId { get; set; }
        public MajorIncidentSummaryDTO? MajorIncident { get; set; }

        public List<TicketCommentDTO> Comments { get; set; } = new();
        public List<TicketEventDTO> Events { get; set; } = new();
    }
}
