using ITPortal.Entities.DTOs.AuthDTOs;
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
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var ua = Request.Headers["User-Agent"].ToString();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            var tokens = await _authService.LoginAsync(loginRequest, ua, ip);
            return Ok(tokens);
        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO refreshRequest)
        {
            var ua = Request.Headers["User-Agent"].ToString();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var tokens = await _authService.RefreshAsync(refreshRequest.RefreshToken, ua, ip);
            return Ok(tokens);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDTO logoutRequest)
        {
            await _authService.LogoutAsync(logoutRequest.RefreshToken);
            return NoContent();
        }
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                userId = CurrentUserId,
                userName = CurrentUserName,
                departmentId = CurrentDepartmentId,
                fullName = CurrentFullName,
                roles = CurrentRoles
            });
        }
    }
}