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
                      .OrderBy(x => x.Role.Name)
                      .Where(ur => ur.UserId == userId)
                      .Select(ur => new UserRoleDTO
                      {
                          RoleId = ur.RoleId,
                          RoleName = ur.Role.Name,
                          AssignedAt = ur.AssignedAt
                      }).ToListAsync();
        }
        public async Task<bool> RemoveAsync(ulong userId, ulong roleId)
        {
            var entity = await _set.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (entity == null) return false;

            _set.Remove(entity);
            return true;
        }
    }
}
