using ITPortal.Entities.DTOs.LookupDTOs;
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

    public class LookupController : BaseApiController
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetLookupTypes([FromQuery] string? search, [FromQuery] int take = 20)
        {
            var result = await _lookupService.GetLookupTypesAsync(search, take);
            return Ok(result);
        }

        [HttpGet("by-type/{typeCode}")]
        public async Task<IActionResult> GetLookupsByType(string typeCode, [FromQuery] string? search, [FromQuery] int take = 50)
        {
            if (string.IsNullOrWhiteSpace(typeCode))
                return BadRequestMsg("typeCode boş olamaz.");

            var result = await _lookupService.GetLookupsByTypeCodeAsync(typeCode, search, take);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(ulong id)
        {
            var result = await _lookupService.GetByIdAsync(id);
            if (result == null)
                return NotFoundMsg("Lookup bulunamadı.");

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin,SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateLookupDTO dto)
        {
            try
            {
                var created = await _lookupService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequestMsg(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestMsg(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SystemAdmin,SuperAdmin")]
        public async Task<IActionResult> Update(ulong id, [FromBody] UpdateLookupDTO dto)
        {
            try
            {
                var updated = await _lookupService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFoundMsg("Lookup bulunamadı.");
            }
            catch (ArgumentException ex)
            {
                return BadRequestMsg(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestMsg(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SystemAdmin,SuperAdmin")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var deleted = await _lookupService.DeleteAsync(id);
            if (!deleted)
                return NotFoundMsg("Lookup bulunamadı.");

            return NoContent();
        }
    }
}