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
    public class UserController: ControllerBase
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetallUsers(int pageNumber, int pageSize)
        {
            var users = await _services.GetUsersWithPaginationAsync(pageNumber, pageSize);
            return Ok(users);
        }
    }
}
