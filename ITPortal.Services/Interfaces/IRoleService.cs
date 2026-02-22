using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IRoleService
    {
        Task<PagedResultDTO<RoleMiniDTO>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<RoleLookupDTO>> GetLookUpAsync(string? search, int take);
        Task<RoleDTO?> GetByIdAsync(ulong id);
        Task<RoleDTO> CreateAsync(CreateRoleDTO dto);
        Task<RoleDTO> UpdateAsync(ulong id, UpdateRoleDTO dto);
        Task<bool> DeleteAsync(ulong id);
    }
}
