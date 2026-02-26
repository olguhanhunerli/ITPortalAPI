using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.BusinessHourDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository
{
    public class BusinessHourRepository : GenericRepository<BusinessHours, ulong>, IBusinessHourRepository
    {
        public BusinessHourRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<BusinessHours> GetBusinessHourByIdAsync(ulong businessId)
        {
           return await _set
                .AsNoTracking()
                .Where(b => b.Id == businessId)
                .Include(b => b.Rules)
                .Include(x => x.SlaPolicies)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BusinessHoursLookupDTO>> GetBusinessHourLookupAsync(string? search, int take)
        {
            if (take <= 0) take = 50;
            if (take > 200) take = 200;
            var query = _set.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.OrderBy(d => d.Name).Take(take).Select(d => new BusinessHoursLookupDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<BusinessHours>> GetBusinessHoursAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var totalCount = await _set.CountAsync();

            var baseQuery = _set
                .AsNoTracking()
                .Include(x => x.SlaPolicies)
                .Include(x => x.Rules)
                .OrderBy(l => l.Id);
            return new PagedResultDTO<BusinessHours>
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
