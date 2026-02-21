using ITPortal.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITPortal.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected const string ClaimUserId = "userId";
        protected const string ClaimUserName = "userName";
        protected const string ClaimDepartmentId = "departmentId";
        protected const string ClaimFullName = "fullName";
      
        protected ulong? CurrentUserId => GetUlongClaim(ClaimUserId);

        protected string CurrentUserName =>
            User?.FindFirst(ClaimUserName)?.Value ?? string.Empty;
       
        protected ulong? CurrentDepartmentId => GetUlongClaim(ClaimDepartmentId);

        protected string CurrentFullName =>
            User?.FindFirst(ClaimFullName)?.Value ?? string.Empty;

        protected IEnumerable<string> CurrentRoles =>
            User?.FindAll(ClaimTypes.Role).Select(x => x.Value) ?? Enumerable.Empty<string>();

        protected bool IsInRole(string roleName) =>
            User?.IsInRole(roleName) ?? false;

        protected string ClientIp =>
            HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;

        protected string UserAgent =>
            Request?.Headers["User-Agent"].ToString() ?? string.Empty;

        protected ulong? GetUlongClaim(string claimType)
        {
            var value = User?.FindFirst(claimType)?.Value;
            if (string.IsNullOrWhiteSpace(value)) return null;
            return ulong.TryParse(value, out var id) ? id : null;
        }
        protected void EnsureAuthenticated()
        {
            if (CurrentUserId == null) throw new UnauthorizedAccessException("UserId claim missing.");
        }
        protected IActionResult NotFoundMsg(string message) =>
            NotFound(new { message });

        protected IActionResult BadRequestMsg(string message) =>
            BadRequest(new { message });

        protected IActionResult ForbiddenMsg(string message = "Forbidden") =>
            StatusCode(StatusCodes.Status403Forbidden, new { message });

        protected IActionResult UnauthorizedMsg(string message = "Unauthorized") =>
            StatusCode(StatusCodes.Status401Unauthorized, new { message });
    }
}
