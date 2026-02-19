using ITPortal.Business.Context;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(RefreshToken token) => _context.RefreshTokens.AddAsync(token).AsTask();


        public Task<RefreshToken?> GetValidByHashAsync(string tokenHash)
        {
            return _context.RefreshTokens
            .Include(x => x.User)
                .ThenInclude(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(x =>
                x.TokenHash == tokenHash &&
                !x.Revoked &&
                x.ExpiresAt > DateTime.UtcNow);
        }

        public Task RevokeAsync(RefreshToken token)
        {
            token.Revoked = true;
            _context.RefreshTokens.Update(token);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
       
    }
}
