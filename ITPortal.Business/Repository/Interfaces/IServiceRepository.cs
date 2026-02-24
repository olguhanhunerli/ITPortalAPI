using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service, ulong>
    {
        Task<PagedResultDTO<ServiceMiniDTO>> GetAllServiceAsync(int pageNumber, int pageSize);
        Task<Service> GetByServiceIdAsync(ulong serviceId);
        Task<ServiceLookupDTO> GetServiceLookupAsync();
    }
}
