using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class Location
    {
        [Key]
        [Column("Id")]
        public ulong Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}
