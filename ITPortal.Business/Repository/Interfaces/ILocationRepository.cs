using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location, ulong>
    {
        Task<PagedResultDTO<Location>> GetLocationsWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<LocationLookUpDTO>> GetLocationLookUpAsync(string? search, int take);
        Task<Location> GetLocationByIdAsync(ulong id);
        Task<bool> ExistsAsync(ulong id);

    }
}
