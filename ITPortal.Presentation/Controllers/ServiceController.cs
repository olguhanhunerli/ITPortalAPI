using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Presentation.Authorization;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetServiceAll(int pageNumber = 1, int pageSize = 10)
        {
            var services = await _serviceService.GetServicesWithPaginationAsync(pageNumber, pageSize);
            return Ok(services);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetServiceById(ulong id)
        {
            var service = await _serviceService.GetByServiceIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }
        [HttpPost]
        [Authorize(Roles = RoleGroups.MasterDataWrite)]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDTO serviceCreateDto)
        {
            var createdService = await _serviceService.CreateServicesAsync(serviceCreateDto);
            return CreatedAtAction(nameof(GetServiceById), new { id = createdService.Id }, createdService);
        }
        [HttpPut("{serviceId}")]
        [Authorize(Roles = RoleGroups.MasterDataWrite)]
        public async Task<IActionResult> UpdateService(ulong serviceId, [FromBody] UpdateServiceDTO serviceUpdateDto)
        {
            var updatedService = await _serviceService.UpdateServiceAsync(serviceId, serviceUpdateDto);
            if (updatedService == null)
            {
                return NotFound();
            }
            return Ok(updatedService);
        }
        [HttpPatch("{serviceId:ulong}/toggle-active")]
        [Authorize(Roles = RoleGroups.MasterDataWrite)]
        public async Task<IActionResult> ToggleActive([FromRoute] ulong serviceId)
        {
            var ok = await _serviceService.UpdateServiceActiveAsync(serviceId);
            if (!ok) return NotFoundMsg("Service bulunamadı");
            return NoContent();
        }
        [HttpDelete("{serviceId}")]
        [Authorize(Roles = RoleGroups.MasterDataWrite)]
        public async Task<IActionResult> DeleteService(ulong serviceId)
        {
            var result = await _serviceService.DeleteServiceAsync(serviceId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("lookup")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetServiceLookup()
        {
            var serviceLookup = await _serviceService.GetLookupServiceAsync();
            return Ok(serviceLookup);
        }
    }
}