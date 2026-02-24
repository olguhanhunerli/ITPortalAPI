using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IServiceService
    {
        Task<PagedResultDTO<ServiceMiniDTO>> GetServicesWithPaginationAsync(int pageNumber, int pageSize);
        Task<ServiceDetailDTO> CreateServicesAsync(CreateServiceDTO createServiceDTO);
        Task<ServiceDetailDTO> GetByServiceIdAsync(ulong serviceId);
        Task<List<ServiceLookupDTO>> GetLookupServiceAsync();
        Task<ServiceDetailDTO> UpdateServiceAsync(ulong serviceId, UpdateServiceDTO updateServiceDTO);
        Task<bool> DeleteServiceAsync(ulong serviceId);
        Task<bool> UpdateServiceActiveAsync(ulong serviceId);
    }
}
