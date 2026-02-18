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

        public async Task<PagedResultDTO<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var totalCount = await _set.CountAsync();

            var baseQuery = _set.AsNoTracking()
                .Include(t => t.Department)
                .Include(t => t.Users);

            return new PagedResultDTO<Team>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
