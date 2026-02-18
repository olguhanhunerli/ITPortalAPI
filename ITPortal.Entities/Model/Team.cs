using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class Team
    {
        [Key]
        [Column("Id")]
        public ulong Id { get; set; }

        public ulong? DepartmentId { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public Department? Department { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
