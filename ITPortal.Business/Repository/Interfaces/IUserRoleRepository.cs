using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.UserRoleDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserRole, ulong>
    {
        Task<bool> ExistsAsync(ulong userId, ulong roleId);
        Task AddAsync(UserRole entity);
        Task<bool> RemoveAsync(ulong userId, ulong roleId);
        Task<List<UserRoleDTO>> GetUserRolesAsync(ulong userId);
    }
}
