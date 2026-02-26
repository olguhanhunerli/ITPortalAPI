using ITPortal.Entities.DTOs.BusinessHourDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IBusinessHourService
    {
        Task<PagedResultDTO<BusinessHoursMiniDTO>> GetBusinessHoursAsync(int pageNumber, int pageSize);
        Task<BusinessHoursDetailDTO> GetBusinessHourByIdAsync(ulong id);
        Task<BusinessHoursDetailDTO> CreateBusinessHourAsync(CreateBusinessHoursDTO businessHourCreateDTO);
        Task<BusinessHoursDetailDTO> UpdateBusinessHourAsync(ulong id, UpdateBusinessHoursDTO businessHourUpdateDTO);
        Task<List<BusinessHoursLookupDTO>> GetBusinessHoursLookupAsync(string? search, int take);
    }
}
