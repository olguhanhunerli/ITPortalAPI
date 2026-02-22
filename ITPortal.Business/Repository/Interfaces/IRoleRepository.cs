using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role, ulong>
    {
        Task<bool> ExistsRoleByNameAsync(string name, ulong? excludeId = null);
        Task<Role?> GetRoleByIdAsync(ulong id);
        Task<PagedResultDTO<Role>> GetRoleWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<RoleLookupDTO>> GetRoleLookUpAsync(string? search, int take);
        Task<Role?> GetByNameAsync(string name);
    }
}
