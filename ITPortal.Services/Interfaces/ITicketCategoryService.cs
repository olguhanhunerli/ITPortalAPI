using ITPortal.Entities.DTOs.TicketCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITicketCategoryService
    {
        Task<List<TicketCategoryLookupDTO>> GetLookupAsync(string? search, int take, bool activeOnly);
        Task<List<TicketCategoryTreeDTO>> GetTreeAsync(bool activeOnly);
        Task<TicketCategoryDTO?> GetByIdAsync(ulong id);
        Task<TicketCategoryDTO> CreateAsync(CreateTicketCategoryDTO dto);
        Task<TicketCategoryDTO> UpdateAsync(ulong id, UpdateTicketCategoryDTO dto);
        Task<bool> DeleteAsync(ulong id);
    }
}
