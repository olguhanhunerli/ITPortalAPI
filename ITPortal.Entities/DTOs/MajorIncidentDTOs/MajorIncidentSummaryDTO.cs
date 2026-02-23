using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.MajorIncidentDTOs
{
    public class MajorIncidentSummaryDTO
    {
        public ulong Id { get; set; }

        public ulong StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public DateTime DeclaredAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public ulong LeadId { get; set; }
        public string LeadName { get; set; } = null!;
    }
}
