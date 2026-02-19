using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetValidByHashAsync(string tokenHash);
        Task RevokeAsync(RefreshToken token);
        Task SaveChangesAsync();
    }
}
