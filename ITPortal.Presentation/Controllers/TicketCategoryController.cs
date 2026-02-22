using ITPortal.Entities.DTOs.TicketCategoryDTOs;
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
    public class TicketCategoryController : BaseApiController
    {
        private readonly ITicketCategoryService _service;

        public TicketCategoryController(ITicketCategoryService service)
        {
            _service = service;
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string? search, [FromQuery] int take, [FromQuery] bool activeOnly)
            => Ok(await _service.GetLookupAsync(search, take, activeOnly));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTicketCategoryId(ulong id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFoundMsg("Category bulunamadı.") : Ok(dto);
        }
        [HttpPost]
        [Authorize(Roles = "SystemAdmin, SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateTicketCategoryDTO dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByTicketCategoryId), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequestMsg(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "SystemAdmin, SuperAdmin")]
        public async Task<IActionResult> Update(ulong id, [FromBody] UpdateTicketCategoryDTO dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return updated == null ? NotFoundMsg("Category bulunamadı.") : Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequestMsg(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SystemAdmin, SuperAdmin")]
        public async Task<IActionResult> Delete(ulong id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return deleted ? Ok("Category silindi.") : NotFoundMsg("Category bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequestMsg(ex.Message);
            }
        }

    }
}