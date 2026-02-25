using ITPortal.Entities.DTOs.ConfigurationItemDTOs;
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
    [Authorize(Roles = RoleGroups.PortalUsers)]
    public class ConfigurationItemController : BaseApiController
    {
        private readonly IConfigurationItemService _configurationItemService;

        public ConfigurationItemController(IConfigurationItemService configurationItemService)
        {
            _configurationItemService = configurationItemService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllConfigurationItem(int pageNumber = 1, int pageSize = 10)
        {
            var items = await _configurationItemService.GetConfigurationItemAsync(pageNumber, pageSize);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfigurationItemById(ulong id)
        {
            var item = await _configurationItemService.GetConfigurationItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> GetConfigurationItemLookup([FromQuery] string? search, [FromQuery] int take = 50)
        {
            var lookup = await _configurationItemService.GetConfigurationItemLookupAsync(search, take);
            return Ok(lookup);
        }


        [Authorize(Roles = RoleGroups.AssetManage)]
        [HttpPost]
        public async Task<IActionResult> CreateConfigurationItem([FromBody] CreateConfigurationItemDTO createDto)
        {
            var createdItem = await _configurationItemService.CreateConfigurationItemAsync(createDto);
            return Ok(createdItem);
        }

        [Authorize(Roles = RoleGroups.AssetManage)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfigurationItem(ulong id, [FromBody] UpdateConfigurationItemDTO updateDto)
        {
            var updatedItem = await _configurationItemService.UpdateConfigurationItemAsync(id, updateDto);
            if (updatedItem == null) return NotFound();
            return Ok(updatedItem);
        }

        [Authorize(Roles = RoleGroups.AssetManage)]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateConfigurationItemStatus(ulong id, [FromQuery] ulong status)
        {
            var ok = await _configurationItemService.UpdateStatusConfigurationItemAsync(id, status);
            if (!ok) return NotFound();
            return Ok();
        }

        [Authorize(Roles = RoleGroups.AssetManage)]
        [HttpPut("{id}/ownerUser")]
        public async Task<IActionResult> UpdateConfigurationItemOwner(ulong id, [FromQuery] ulong ownerUserId)
        {
            var updatedItem = await _configurationItemService.UpdateConfigurationItemOwnerUserAsync(id, ownerUserId);
            if (updatedItem == null) return NotFound();
            return Ok(updatedItem);
        }

        [Authorize(Roles = RoleGroups.AssetManage)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigurationItem(ulong id)
        {
            var result = await _configurationItemService.DeleteConfigurationItemAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}