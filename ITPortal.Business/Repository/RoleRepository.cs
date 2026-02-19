using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.RoleDTOs;
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
    public class RoleRepository : GenericRepository<Role, ulong>, IRoleRepository
    {
        public RoleRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<bool> ExistsRoleByNameAsync(string name, ulong? excludeId = null)
        {
            name = name.Trim();

            var q = _set.AsNoTracking().Where(r => r.Name == name);
            if (excludeId != null) q = q.Where(r => r.Id != excludeId.Value);
            return await q.AnyAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(ulong id)
        {
            return await _set.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<RoleLookupDTO>> GetRoleLookUpAsync(string? search, int take)
        {
            if (take <= 50) take = 50;
            if (take > 200) take = 200;

            var q = _set.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                q = q.Where(r => r.Name.Contains(search));
            }
            return q.OrderBy(r => r.Name).Take(take).Select(r => new RoleLookupDTO
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<Role>> GetRoleWithPaginationAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var baseQuery = _set.AsNoTracking().OrderBy(r => r.Name);

            var totalCount = await baseQuery.CountAsync();

            return new PagedResultDTO<Role>()
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
