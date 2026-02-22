using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, ulong>
    {
        Task<PagedResultDTO<User>> GetUsersWithPaginationAsync(int pageNumber, int pageSize);
        Task<User?> GetUserByEmailWithDetailsAsync(string email);
        Task<List<UserLookUpDTO>> GetUserLookUpAsync(string? search, int take);
    }
}
