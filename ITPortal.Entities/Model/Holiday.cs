using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class Holiday
    {
        public ulong Id { get; set; }

        public string NameTr { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public DateTime HolidayDate { get; set; }   

        public ulong? LocationId { get; set; }

        public bool IsFullDay { get; set; } = true;

        public DateTime CreatedAt { get; set; }


        // Navigation
        public Location? Location { get; set; }
    }
}
