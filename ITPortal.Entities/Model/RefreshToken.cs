using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public User User { get; set; }

        public string TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Revoked { get; set; } = false;
        public string? UserAgent { get; set; }
        public string? IpAddress { get; set; }
    }
}
