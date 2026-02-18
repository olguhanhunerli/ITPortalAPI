using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<LocationMiniDTO> CreateLocationAsync(CreateLocationDTO dto)
        {
            var locationEntity = _mapper.Map<Location>(dto);
            locationEntity.CreatedAt = DateTime.UtcNow;
            await _locationRepository.AddAsync(locationEntity);
            await _locationRepository.SaveChangesAsync();
            return _mapper.Map<LocationMiniDTO>(locationEntity);
        }

        public async Task<bool> DeleteLocationAsync(ulong id)
        {
            var locationEntity = await _locationRepository.GetByIdAsync(id);
            if (locationEntity == null) 
            {
                return false;
            }
            _locationRepository.Remove(locationEntity);
            await _locationRepository.SaveChangesAsync();
            return true;

        }

        public async Task<LocationDTO> GetLocationByIdAsync(ulong id)
        {
            var locationEntity = await _locationRepository.GetByIdAsync(id);
            return _mapper.Map<LocationDTO>(locationEntity);
        }

        public async Task<List<LocationLookUpDTO>> GetLocationLookUpAsync(string? search, int take)
        {
            return await _locationRepository.GetLocationLookUpAsync(search, take);
        }

        public async Task<PagedResultDTO<LocationMiniDTO>> GetLocationsWithPaginationAsync(int pageNumber, int pageSize)
        {
           var pagedLocations = await _locationRepository.GetLocationsWithPaginationAsync(pageNumber, pageSize);
           var mappedLocations = _mapper.Map<List<LocationMiniDTO>>(pagedLocations.Items);

            return new PagedResultDTO<LocationMiniDTO>
            {
                TotalCount = pagedLocations.TotalCount,
                Page = pagedLocations.Page,
                PageSize = pagedLocations.PageSize,
                Items = mappedLocations
            };
        }

        public async Task<LocationMiniDTO> UpdateLocationAsync(ulong id ,UpdateLocationDTO dto)
        {
            var locationEntity = await _locationRepository.GetByIdAsync(id);

            if (locationEntity == null)
            {
                return null;
            }
            _locationRepository.Update(locationEntity);
            await _locationRepository.SaveChangesAsync();
            return _mapper.Map<LocationMiniDTO>(locationEntity);
        }
    }
}
