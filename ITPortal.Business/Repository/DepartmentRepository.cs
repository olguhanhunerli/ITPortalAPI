using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
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
    public class DepartmentRepository : GenericRepository<Department, ulong>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<bool> ExistsAsync(ulong id)
        {
           return await _set.AnyAsync(d => d.Id == id);
        }

        public async Task<Department> GetDepartmentByIdAsync(ulong id)
        {
            return await _set.AsNoTracking()
                .Include(d => d.Users)
                .Include(d => d.Teams)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<DepartmentLookUpDTO>> GetDepartmentLookUpAsync(string? search, int take)
        {
            if (take <= 0) take = 50;
            if(take > 200) take = 200;
            var query = _set.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.OrderBy(d => d.Name).Take(take).Select(d => new DepartmentLookUpDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
        }

        public async Task<PagedResultDTO<Department>> GetDepartmentsWithPaginationAsync(int pageNumber, int pageSize)
        {
            if(pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if(pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if(pageSize >100) pageSize = 100;
            
            var baseQuery = _set
                .AsNoTracking()
                .Include(d => d.Users)
                .Include(d => d.Teams)
                .OrderBy(d => d.Name);
            var totalCount = await _set.CountAsync();

            return new PagedResultDTO<Department>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
