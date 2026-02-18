using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.LocationDTOs;
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
    }
}
