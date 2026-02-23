using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/[controller]")]

    public class UserRoleController : BaseApiController
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService? userRoleService)
        {
            _userRoleService = userRoleService;
        }
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(ulong userId)
        {
           return Ok(await _userRoleService.GetUserRolesAsync(userId));
        }

        [HttpPost("{userId}/roles/{roleId}")]
        public async Task<IActionResult> AssignRole(ulong userId, ulong roleId)
        {
            var added = await _userRoleService.AssignRoleAsync(userId, roleId);
            return added ? Ok("Role atandı") : Conflict("Role zaten atanmış");
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveRole(ulong userId, ulong roleId)
        {
            var removed = await _userRoleService.RemoveUserRoleAsync(userId, roleId);
            return removed ? NoContent() : NotFound("UserRole bulunamadı");
        }

    }
}
