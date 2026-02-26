using ITPortal.Entities.DTOs.BusinessHourDTOs;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/admin/business-hours")]
    public class BusinessHourController : BaseApiController
    {
        private readonly IBusinessHourService _businessHourService;

        public BusinessHourController(IBusinessHourService businessHourService)
        {
            _businessHourService = businessHourService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBusinessHours(int page = 1, int pageSize = 10)
        {
            var businessHours = await _businessHourService.GetBusinessHoursAsync(page, pageSize);
            return Ok(businessHours);
        }
        [HttpGet("{businessHourId}")]
        public async Task<IActionResult> GetBusinessHourById(ulong businessHourId)
        {
            var businessHour = await _businessHourService.GetBusinessHourByIdAsync(businessHourId);
            if (businessHour == null)
                return NotFoundMsg("Bulunamadı");
            return Success(businessHour, "Başarıyla getirildi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusinessHour([FromBody] CreateBusinessHoursDTO request)
        {
            var createdBusinessHour = await _businessHourService.CreateBusinessHourAsync(request);
            return CreatedAtAction(nameof(GetBusinessHourById), new { businessHourId = createdBusinessHour.Id }, createdBusinessHour);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetBusinessHoursLookup(string? search, int take = 10)
        {
            var lookupData = await _businessHourService.GetBusinessHoursLookupAsync(search, take);
            return Ok(lookupData);
        }
        [HttpPut("{businessHourId}")]
        public async Task<IActionResult> UpdateBusinessHour(ulong businessHourId, [FromBody] UpdateBusinessHoursDTO request)
        {
            var updatedBusinessHour = await _businessHourService.UpdateBusinessHourAsync(businessHourId, request);
            if (updatedBusinessHour == null)
                return NotFoundMsg("Bulunamadı");
            return Ok(updatedBusinessHour);
        }
    }
}