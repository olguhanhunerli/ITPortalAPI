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
        public async Task<IActionResult> GetallDepartments(int pageNumber, int pageSize)
        {
            var departments = await _services.GetDepartmentsWithPaginationAsync(pageNumber, pageSize);
            return Ok(departments);
        }
    }
}
