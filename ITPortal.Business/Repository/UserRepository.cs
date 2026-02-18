using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
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
    public class UserRepository : GenericRepository<User, ulong>, IUserRepository
    {
        public UserRepository(AppDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
        }

        public async Task<PagedResultDTO<User>> GetUsersWithPaginationAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than zero.", nameof(pageNumber));
            if (pageSize <= 0)
                throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
            if (pageSize > 100) pageSize = 100;
            var baseQuery = _set
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .OrderBy(u => u.FullName);
            var totalCount = await baseQuery.CountAsync();
            return new PagedResultDTO<User>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
