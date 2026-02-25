using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.ConfigurationItemDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;

namespace ITPortal.Services
{
    public class ConfigurationItemService : IConfigurationItemService
    {
        private readonly IConfigurationItemsRepository _configurationItemsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILookupRepository _lookupRepository;

        public ConfigurationItemService(IConfigurationItemsRepository configurationItemsRepository, IUserRepository userRepository, ITeamRepository teamRepository, IMapper mapper, ILookupRepository lookupRepository)
        {
            _configurationItemsRepository = configurationItemsRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
            _lookupRepository = lookupRepository;
        }

        public async Task<ConfigurationItemDetailDTO> CreateConfigurationItemAsync(CreateConfigurationItemDTO createDTO)
        {
            var now = DateTime.Now;
            var entity = _mapper.Map<ConfigurationItem>(createDTO);
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            await _configurationItemsRepository.AddAsync(entity);
            await _configurationItemsRepository.SaveChangesAsync();
            var created = await _configurationItemsRepository.GetConfigurationItemByIdAsync(entity.Id);
            if (created == null) throw new Exception("Service oluşturuldu ama okunamadı.");

            return _mapper.Map<ConfigurationItemDetailDTO>(created);
        }

        public async Task<bool> DeleteConfigurationItemAsync(ulong id)
        {
            var entity = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"ConfigurationItem with ID {id} not found.");

            _configurationItemsRepository.Remove(entity);
            await _configurationItemsRepository.SaveChangesAsync();

            return true;

        }

        public async Task<PagedResultDTO<ConfigurationItemMiniDTO>> GetConfigurationItemAsync(int pageNumber, int pageSize)
        {
            var entity = await _configurationItemsRepository.GetConfigurationItemsAsync(pageNumber, pageSize);

            return new PagedResultDTO<ConfigurationItemMiniDTO>
            {
                TotalCount = entity.TotalCount,
                Page = entity.Page,
                PageSize = entity.PageSize,
                Items = _mapper.Map<List<ConfigurationItemMiniDTO>>(entity.Items)
            };
        }

        public async Task<ConfigurationItemDetailDTO> GetConfigurationItemByIdAsync(ulong id)
        {
            var entity = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"ConfigurationItem with ID {id} not found.");
            return _mapper.Map<ConfigurationItemDetailDTO>(entity);
        }

        public async Task<List<ConfigurationItemLookupDTO>> GetConfigurationItemLookupAsync(string? search, int take)
        {
            return await _configurationItemsRepository.GetConfigurationItemLookupAsync(search, take);
        }
        

        public async Task<ConfigurationItemDetailDTO> UpdateConfigurationItemAsync(ulong id, UpdateConfigurationItemDTO updateDTO)
        {
            var item = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);
            if (item == null) throw new KeyNotFoundException($"ConfigurationItem with ID {id} not found.");

            item.LocationId = updateDTO.LocationId;
            item.Name = updateDTO.Name;
            item.NameTr = updateDTO.NameTr;
            item.OwnerTeamId = updateDTO.OwnerTeamId;
            item.OwnerUserId = updateDTO.OwnerUserId;
            item.SerialNumber = updateDTO.SerialNumber;
            item.AssetTag = updateDTO.AssetTag;
            item.CiStatusId = updateDTO.CiStatusId;
            item.CiTypeId = updateDTO.CiTypeId;
            item.UpdatedAt = DateTime.UtcNow;

            _configurationItemsRepository.Update(item);
            await _configurationItemsRepository.SaveChangesAsync();
            var updated = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);
            if (updated == null) throw new Exception("Güncellenmiş ConfigurationItem okunamadı.");
            return _mapper.Map<ConfigurationItemDetailDTO>(updated);
        }

        public async Task<ConfigurationItemDetailDTO> UpdateConfigurationItemOwnerUserAsync(ulong id, ulong ownerUserId)
        {
            var entity = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("ConfigurationItem with ID {id} not found.");

            var ownerUser = await _userRepository.GetUserByIdWithDetailsAsync(ownerUserId);
            if (ownerUserId != null && ownerUser == null) throw new KeyNotFoundException($"User with ID {ownerUserId} not found.");

            entity.OwnerUserId = ownerUserId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.OwnerTeamId = ownerUserId != null ? ownerUser!.TeamId : null;
            entity.LocationId = ownerUserId != null ? ownerUser!.LocationId : null;
            _configurationItemsRepository.Update(entity);
            await _configurationItemsRepository.SaveChangesAsync();

            var updated = await _configurationItemsRepository.GetConfigurationItemByIdAsync(id);

            return _mapper.Map<ConfigurationItemDetailDTO>(updated);

        }

        public async Task<bool> UpdateStatusConfigurationItemAsync(ulong id, ulong ciStatusId)
        {
            var entity = await _configurationItemsRepository.GetConfigurationItemByIdForUpdateAsync(id);
            if (entity == null) return false;

            entity.CiStatusId = ciStatusId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _configurationItemsRepository.SaveChangesAsync();
            return true;
        }
    }
}
