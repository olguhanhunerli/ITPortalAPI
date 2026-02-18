using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.DTOs.UserDTOs
{
    public class UserLookUpDTO
    {
        public ulong Id { get; set; }
        public string FullName { get; set; } = default!;
    }
}
