using ITPortal.Entities.DTOs.DepartmentDTOs;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _services;

        public DepartmentController(IDepartmentService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetallDepartments(int pageNumber = 1, int pageSize = 10)
        {
            var departments = await _services.GetDepartmentsWithPaginationAsync(pageNumber, pageSize);
            return Ok(departments);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetDepartmentLookup(string? search, int take = 50)
        {
            var departmentLookUp = await _services.GetDepartmentLookUpAsync(search, take);
            return Ok(departmentLookUp);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO createDepartmentDTO)
        {
            var createdDepartment = await _services.CreateDepartmentAsync(createDepartmentDTO);
            return Ok(createdDepartment);
        }
    }
}
