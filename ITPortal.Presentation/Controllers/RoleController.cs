using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles(int pageNumber = 1, int pageSize = 10)
        {
            var roles = await _roleService.GetWithPaginationAsync(pageNumber, pageSize);
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(ulong id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetRolesLookUp(string? search, int take = 10)
        {
            var roles = await _roleService.GetLookUpAsync(search, take);
            return Ok(roles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDTO dto)
        {
            var createdRole = await _roleService.CreateAsync(dto);
            return Ok(await _roleService.GetByIdAsync(createdRole.Id));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole(ulong id, UpdateRoleDTO dto)
        {
            var updatedRole = await _roleService.UpdateAsync(id, dto);
            if (updatedRole == null)
            {
                return NotFound();
            }
            return Ok(updatedRole);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(ulong id)
        {
            var deleted = await _roleService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
