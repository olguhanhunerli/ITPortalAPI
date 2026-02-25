using ITPortal.Entities.DTOs.ConfigurationItemDTOs;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IConfigurationItemService
    {
        Task<ConfigurationItemDetailDTO> GetConfigurationItemByIdAsync(ulong id);
        Task<List<ConfigurationItemLookupDTO>> GetConfigurationItemLookupAsync(string? search, int take);
        Task<PagedResultDTO<ConfigurationItemMiniDTO>> GetConfigurationItemAsync(int pageNumber, int pageSize);

        Task<ConfigurationItemDetailDTO> CreateConfigurationItemAsync(CreateConfigurationItemDTO createDTO);
        Task<ConfigurationItemDetailDTO> UpdateConfigurationItemAsync(ulong id, UpdateConfigurationItemDTO updateDTO);
        Task<bool> DeleteConfigurationItemAsync(ulong id);
        Task<ConfigurationItemDetailDTO> UpdateConfigurationItemOwnerUserAsync(ulong id, ulong ownerUserId);
        Task<bool> UpdateStatusConfigurationItemAsync(ulong id, ulong ciStatusId);

    }
}
