using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketCategoryDTOs;
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
    public class TicketCategoryRepository : GenericRepository<TicketCategory, ulong>, ITicketCategoryRepository
    {
        public TicketCategoryRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public Task<bool> ExistsCategoryCodeAsync(string code, ulong? exludeId = null)
        {
            code = code.Trim();

            var query = _set
                .AsNoTracking()
                .Where(x => x.Code == code && x.DeletedAt == null);
            if(exludeId.HasValue)
            {
                query = query.Where(x => x.Id == exludeId.Value);
            }
            return query.AnyAsync();
        }

        public Task<List<TicketCategory>> GetAllForTreeAsync(bool activeOnly)
        {
           var query = _set
                .AsNoTracking()
                .Include(x => x.DefaultTeam)
                .Where(x => x.DeletedAt == null);
            if (activeOnly)
            {
                query = query.Where(x => x.IsActive);
            }
            return query
                .OrderBy(x => x.Code)
                .ToListAsync();
        }

        public Task<TicketCategory?> GetByIdWithParentTeamAsync(ulong id)
        {
            return _set
                .Include(x => x.Parent)
                .Include(x => x.DefaultTeam)
                .FirstOrDefaultAsync(x => EF.Property<ulong>(x, "Id") == id && x.DeletedAt == null);
        }

        public async Task<List<TicketCategoryLookupDTO>> GetCategoryLookUpAsync(string? search, int take, bool activeOnly)
        {
            if (take <= 0) take = 20;
            if (take > 200) take = 200;

            var query = _set.AsNoTracking()
                .Where(x => x.DeletedAt == null);

            if (activeOnly)
                query = query.Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x =>
                    x.Code.Contains(search) ||
                    x.NameTr.Contains(search) ||
                    x.NameEn.Contains(search));
            }

            var list = await query
                .OrderBy(x => x.Code)
                .Take(take)
                .Select(x => new
                {
                    x.Id,
                    x.ParentId,
                    x.Code,
                    x.NameTr,
                    x.NameEn,
                    x.IsActive
                })
                .ToListAsync();

            var dict = list.ToDictionary(
                x => x.Id,
                x => new TicketCategoryLookupDTO
                {
                    Id = x.Id,
                    Code = x.Code,
                    NameTr = x.NameTr,
                    NameEn = x.NameEn,
                    IsActive = x.IsActive,
                    Children = new List<TicketCategoryLookupDTO>()
                });

            foreach (var item in list)
            {
                if (item.ParentId.HasValue && dict.TryGetValue(item.ParentId.Value, out var parent))
                    parent.Children.Add(dict[item.Id]);
            }

            // rootlar: parent'ı listte olmayan veya null olanlar
            var roots = list
                .Where(x => !x.ParentId.HasValue || !dict.ContainsKey(x.ParentId.Value))
                .Select(x => dict[x.Id])
                .ToList();

            return roots;
        }
    }
}
