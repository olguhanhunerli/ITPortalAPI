using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.LookupDTOs
{
    public class LookupLookupDTO
    {
        public ulong Id { get; set; }
        public string Code { get; set; }
        public string NameTr { get; set; }
        public string NameEn { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
