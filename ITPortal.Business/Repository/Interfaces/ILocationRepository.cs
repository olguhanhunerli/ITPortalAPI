using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.Model;
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
    }
}
