using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _services;

        public LocationController(ILocationService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetallLocations(int pageNumber, int pageSize)
        {
            var locations = await _services.GetLocationsWithPaginationAsync(pageNumber, pageSize);
            return Ok(locations);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetLocationLookup(string? search, int take = 50)
        {
            var locationLookUp = await _services.GetLocationLookUpAsync(search, take);
            return Ok(locationLookUp);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDTO locationDto)
        {
            var createdLocation = await _services.CreateLocationAsync(locationDto);
            return Ok(createdLocation);
        }
    }
}
