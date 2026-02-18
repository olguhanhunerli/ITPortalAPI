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
    }
}
