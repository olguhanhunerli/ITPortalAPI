using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.ConfigurationItemDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IConfigurationItemsRepository : IGenericRepository<ConfigurationItem, ulong>
    {
        Task<PagedResultDTO<ConfigurationItem>> GetConfigurationItemsAsync(int pageNumber, int pageSize);
        Task<ConfigurationItem> GetConfigurationItemByIdAsync(ulong id);
        Task<List<ConfigurationItemLookupDTO>> GetConfigurationItemLookupAsync(string? search, int take);
        Task<ConfigurationItem> GetConfigurationItemByIdForUpdateAsync(ulong id);
        Task<bool> IsConfigurationItemOwnedByUserAsync(ulong configurationItemId, ulong userId);
    }
}
