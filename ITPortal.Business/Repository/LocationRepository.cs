using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
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
    public class LocationRepository : GenericRepository<Location, ulong>, ILocationRepository
    {
        public LocationRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<bool> ExistsAsync(ulong id)
        {
            return await _set.AnyAsync(l => l.Id == id);
        }

        public Task<Location> GetLocationByIdAsync(ulong id)
        {
            return _set
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<LocationLookUpDTO>> GetLocationLookUpAsync(string? search, int take)
        {
            if (take <= 0) take = 50;
            if (take > 200) take = 200;
            var query = _set.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.OrderBy(d => d.Name).Take(take).Select(d => new LocationLookUpDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<Location>> GetLocationsWithPaginationAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var totalCount = await _set.CountAsync();

            var baseQuery = _set
                .AsNoTracking()
                .Include(x => x.Users)
                .OrderBy(l => l.Id);
            return new PagedResultDTO<Location>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
            };
        }
    }
}
