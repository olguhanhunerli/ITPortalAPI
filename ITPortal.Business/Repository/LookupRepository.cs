using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
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

    public class LookupRepository : GenericRepository<Lookup, ulong>, ILookupRepository
    {
        private readonly AppDbContext _context;
        public LookupRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string typeCode, string code, ulong? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(typeCode))
                throw new ArgumentException("Type code boş olamaz", nameof(typeCode));
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code boş olamaz", nameof(code));

            var query = _set.AsNoTracking()
                .Include(x => x.LookupType)
                .Where(x =>
                    x.DeletedAt == null &&
                    x.IsActive &&
                    x.LookupType != null &&
                    x.LookupType.DeletedAt == null &&
                    x.LookupType.Code == typeCode &&
                    x.Code == code);
            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public Task<List<Lookup>> GetLookupsByTypeCodeAsync(string typeCode, string? search, int take)
        {
            if (string.IsNullOrWhiteSpace(typeCode))
                throw new ArgumentException("Type code boş olamaz", nameof(typeCode));
            if (take <= 0) take = 50;
            if (take > 200) take = 200;

            var query = _set.AsNoTracking()
                .Include(x => x.LookupType)
                .Where(x =>
                    x.DeletedAt == null &&
                    x.IsActive &&
                    x.LookupType != null &&
                    x.LookupType.DeletedAt == null &&
                    x.LookupType.Code == typeCode);
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(x => x.NameTr.ToLower().Contains(search) || x.NameEn.ToLower().Contains(search) || x.Code.ToLower().Contains(search));
            }

            return query.OrderBy(x => x.SortOrder).ThenBy(x => x.NameTr).Take(take).ToListAsync();
        }

        public async Task<List<LookupType>> GetLookupTypesAsync(string? search, int take)
        {
            if (take <= 0) take = 20;
            if (take > 200) take = 200;

            var q = _context.LookupTypes
                .AsNoTracking()
                .Where(x => x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                q = q.Where(x => x.NameTr.ToLower().Contains(search) || x.NameEn.ToLower().Contains(search));
            }

            return await q.OrderBy(x => x.NameTr).Take(take).ToListAsync();

        }
    }
}
