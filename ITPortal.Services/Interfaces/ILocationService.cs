using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ILocationService
    {
        Task<PagedResultDTO<LocationMiniDTO>> GetLocationsWithPaginationAsync(int pageNumber, int pageSize);
        Task<LocationMiniDTO> CreateLocationAsync(CreateLocationDTO dto);
        Task<List<LocationLookUpDTO>> GetLocationLookUpAsync(string? search, int take);

    }
}
