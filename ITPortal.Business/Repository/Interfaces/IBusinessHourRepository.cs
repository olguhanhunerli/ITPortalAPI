using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.BusinessHourDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IBusinessHourRepository : IGenericRepository<BusinessHours, ulong>
    {
        Task<PagedResultDTO<BusinessHours>> GetBusinessHoursAsync(int pageNumber, int pageSize);
        Task<BusinessHours> GetBusinessHourByIdAsync(ulong businessId);
        Task<List<BusinessHoursLookupDTO>> GetBusinessHourLookupAsync(string? search , int take);
    }
}
