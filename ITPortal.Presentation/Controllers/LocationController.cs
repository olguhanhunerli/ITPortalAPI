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
        public async Task<IActionResult> GetallLocations(int pageNumber = 1, int pageSize = 10)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(ulong id)
        {
            var location = await _services.GetLocationByIdAsync(id);
            if (location == null)
                return NotFound();
            return Ok(location);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(ulong id, [FromBody] UpdateLocationDTO locationDto)
        {
            var updatedLocation = await _services.UpdateLocationAsync(id, locationDto);
            return Ok(updatedLocation);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(ulong id)
        {
            var result = await _services.DeleteLocationAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
