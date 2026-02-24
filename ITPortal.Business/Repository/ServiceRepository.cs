using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
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
    public class ServiceRepository : GenericRepository<Service, ulong>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<PagedResultDTO<ServiceMiniDTO>> GetAllServiceAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var query = _set.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Where(x=> x.IsActive == true)
                .OrderBy(t => t.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new ServiceMiniDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    NameTr = t.NameTr,  
                    OwnerTeamName = t.OwnerTeam.Name,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();

            return new PagedResultDTO<ServiceMiniDTO>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = items
            };
        }

        public async Task<Service> GetByServiceIdAsync(ulong serviceId)
        {
            return await _set
                .Where(x => x.Id == serviceId && x.IsActive == true)
                .Include(s => s.OwnerTeam)
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<List<ServiceLookupDTO>> GetServiceLookupAsync()
        {
            return await _set.AsNoTracking()
                .Where(x => x.IsActive == true)
                .Select(s => new ServiceLookupDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    NameTr = s.NameTr
                })
                .ToListAsync();
        }
    }
}
