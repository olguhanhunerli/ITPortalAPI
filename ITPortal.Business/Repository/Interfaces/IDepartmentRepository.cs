using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department, ulong>
    {
        Task<PagedResultDTO<Department>> GetDepartmentsWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<DepartmentLookUpDTO>> GetDepartmentLookUpAsync(string? search, int take);
        Task<Department> GetDepartmentByIdAsync(ulong id);
        Task<bool> ExistsAsync(ulong id);
    }
}
