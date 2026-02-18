using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<PagedResultDTO<DepartmentMiniDTO>> GetDepartmentsWithPaginationAsync(int pageNumber, int pageSize);
        Task<DepartmentMiniDTO> CreateDepartmentAsync(CreateDepartmentDTO createDepartmentDTO);
        Task<List<DepartmentLookUpDTO>> GetDepartmentLookUpAsync(string? search, int take);

    }
}
