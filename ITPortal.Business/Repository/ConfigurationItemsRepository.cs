using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.ConfigurationItemDTOs;
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
    public class ConfigurationItemsRepository : GenericRepository<ConfigurationItem, ulong>, IConfigurationItemsRepository
    {
        public ConfigurationItemsRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public Task<ConfigurationItem> GetConfigurationItemByIdAsync(ulong id)
        {
            return _set
                   .AsNoTracking()
                    .Include(x => x.OwnerUser)
                    .Include(x => x.CiType)
                    .Include(x => x.CiStatus)
                    .Include(x => x.Location)
                    .Include(x => x.OwnerTeam)
                   .FirstOrDefaultAsync(l => l.Id == id);
        }

        public Task<ConfigurationItem> GetConfigurationItemByIdForUpdateAsync(ulong id)
        {
            return _set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ConfigurationItemLookupDTO>> GetConfigurationItemLookupAsync(string? search, int take)
        {
            if (take <= 0) take = 50;
            if (take > 200) take = 200;
            var query = _set.AsNoTracking();

            
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.OrderBy(d => d.Name).Take(take).Select(d => new ConfigurationItemLookupDTO
            {
                CiTypeId = d.CiTypeId,
                CiStatusId = d.CiStatusId,
                LocationId = d.LocationId,
                OwnerTeamId = d.OwnerTeamId,
                OwnerUserId = d.OwnerUserId,

                CiTypeName = d.CiType != null ? d.CiType.NameTr : null,
                CiStatusName = d.CiStatus != null ? d.CiStatus.NameTr : null,
                LocationName = d.Location != null ? d.Location.Name : null,
                OwnerTeamName = d.OwnerTeam != null ? d.OwnerTeam.Name : null,
                OwnerUserName = d.OwnerUser != null ? d.OwnerUser.FullName : null,

                AssetTag = d.AssetTag,
                Name = d.Name,
                NameTr = d.NameTr,
                SerialNumber = d.SerialNumber
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<ConfigurationItem>> GetConfigurationItemsAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var totalCount = await _set.CountAsync();

            var baseQuery = _set
                .AsNoTracking()
                .Include(x => x.OwnerUser)
                .Include(x => x.CiType)
                .Include(x => x.CiStatus)
                .Include(x => x.Location)
                .Include(x => x.OwnerTeam)
                .OrderBy(l => l.Id);
            return new PagedResultDTO<ConfigurationItem>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
