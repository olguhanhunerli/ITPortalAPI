using ITPortal.Entities.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class LookupType : AuditableEntity<ulong>
    {
        public string Code { get; set; }
        public string NameTr { get; set; }
        public string NameEn { get; set; }
        public ICollection<Lookup> Lookups { get; set; } = new List<Lookup>();
    }
}
