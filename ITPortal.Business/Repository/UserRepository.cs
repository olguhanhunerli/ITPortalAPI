using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ITPortal.Business.Repository
{
    public class UserRepository : GenericRepository<User, ulong>, IUserRepository
    {
        public UserRepository(AppDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
        }

        public Task<User?> GetUserByEmailWithDetailsAsync(string email)
        {
           return _set
                .AsNoTracking()
                .Include(u => u.Department)
                .Include(u => u.Location)
                .Include(u => u.Team)
                .Include(u => u.UserRoles)
                  .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.DeletedAt == null);
        }

        public Task<List<UserLookUpDTO>> GetUserLookUpAsync(string? search, int take)
        {
            if (take <= 0) take = 10;
            if(take > 200) take = 200;
            var query = _set
                .AsNoTracking()
                
                .Where(u => u.DeletedAt == null);
            if (!string.IsNullOrEmpty(search))
                {
                search = search.ToLower();
                query = query.Where(u => u.FullName.ToLower().Contains(search) || u.Email.ToLower().Contains(search));
            }
            return query
                .OrderBy(u => u.FullName)

                .Select(u => new UserLookUpDTO
                {
                    Id = u.Id,
                    FullName = u.FullName
                })
                .Take(take)
                .ToListAsync();
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
                .Include(d => d.Department)
                .Include(d => d.Team)
                .Include(d => d.Location)
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
