using ITPortal.Entities.DTOs.UserRoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<List<UserRoleDTO>> GetUserRolesAsync(ulong userId);
        Task<bool> AssignRoleAsync(ulong userId, ulong roleId);
        Task<bool> RemoveUserRoleAsync(ulong userId, ulong roleId);
    }
}
