using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
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
    public class TeamRepository : GenericRepository<Team, ulong>, ITeamRepository
    {
        public TeamRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<bool> ExistsAsync(ulong id)
        {
            return await _set.AnyAsync(t => t.Id == id);
        }

        public Task<Team> GetTeamByIdAsync(ulong id)
        {
            return _set.AsNoTracking()
                .Include(t => t.Department)
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TeamLookUpDTO>> GetTeamLookUpAsync(string? search, int take)
        {
            if (take <= 0) take = 50;
            if (take > 200) take = 200;
            var query = _set.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.OrderBy(d => d.Name).Take(take).Select(d => new TeamLookUpDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<TeamMiniDTO>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var query = _set.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(t => t.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TeamMiniDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department != null ? t.Department.Name : null,
                    UserCount = t.Users.Count() 
                })
                .ToListAsync();

            return new PagedResultDTO<TeamMiniDTO>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = items
            };
        }

        public async Task<bool> TeamBelongsToDepartmentAsync(ulong teamId, ulong departmentId)
        {
            return await _set.AnyAsync(t => t.Id == teamId && t.DepartmentId == departmentId);
        }
    }
}
