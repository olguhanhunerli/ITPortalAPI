using ITPortal.Entities.DTOs.LookupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ILookupService
    {
        Task<List<LookupTypeLookupDTO>> GetLookupTypesAsync(string? search, int take);

        Task<List<LookupLookupDTO>> GetLookupsByTypeCodeAsync(string typeCode, string? search, int take);

        Task<LookupDTO?> GetByIdAsync(ulong id);

        Task<LookupDTO> CreateAsync(CreateLookupDTO dto);

        Task<LookupDTO> UpdateAsync(ulong id, UpdateLookupDTO dto);

        Task<bool> DeleteAsync(ulong id);
    }
}
