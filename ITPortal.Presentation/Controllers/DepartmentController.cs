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
    
    public class DepartmentController : BaseApiController
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(ulong id)
        {
            var department = await _services.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound();
            return Ok(department);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDepartment(ulong id, UpdateDepartmentDTO updateDepartmentDTO)
        {
            var updatedDepartment = await _services.UpdateDepartmentAsync(id, updateDepartmentDTO);
            return Ok(updatedDepartment);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO createDepartmentDTO)
        {
            var createdDepartment = await _services.CreateDepartmentAsync(createDepartmentDTO);
            return Ok(createdDepartment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(ulong id)
        {
            await _services.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
