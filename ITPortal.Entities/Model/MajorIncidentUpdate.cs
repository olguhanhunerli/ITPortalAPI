using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class MajorIncidentUpdate
    {
        public ulong Id { get; set; }
        public ulong MajorIncidentId { get; set; }
        public ulong AuthorId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public MajorIncident MajorIncident { get; set; }
        public User Author { get; set; }
    }
}
