using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.UserRoleDTOs;
using ITPortal.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole, ulong>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task AddAsync(UserRole entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task<bool> ExistsAsync(ulong userId, ulong roleId)
        {
            return await _set.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<List<UserRoleDTO>> GetUserRolesAsync(ulong userId)
        {
            return await _set
                .AsNoTracking()
                .Where(x => x.UserId == userId )
                .Include(x => x.Role)
                .Select(x => new UserRoleDTO
                {
                    UserId = x.UserId,
                    RoleId = x.RoleId,
                    RoleName = x.Role != null ? x.Role.Name : null,
                    AssignedAt = x.AssignedAt
                })
                .ToListAsync();
        }
        public async Task<bool> RemoveAsync(ulong userId, ulong roleId)
        {
            var entity = await _set
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (entity == null) return false;

            Remove(entity);
            return true;
        }
    }
}
