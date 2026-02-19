using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITPortal.Presentation.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetallUsers(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _services.GetUsersWithPaginationAsync(pageNumber, pageSize);
            return Ok(users);
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _services.GetUserByEmailWithDetailsAsync(email);
            if (user == null)
            {
                return NotFound("Email Bulunamadı");
            }
            return Ok(user);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetUserLookUp(string? search, int take = 50)
        {
            var user = await _services.GetUserLookUpAsync(search, take);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (createUserDTO.DepartmentId == null) return BadRequest("Department boş geçilemez");
            if (createUserDTO.TeamId == null) return BadRequest("Team boş geçilemez");
            if (createUserDTO.LocationId == null) return BadRequest("Location boş geçilemez");

            var createdUser = await _services.CreateUserAsync(createUserDTO);
            return Ok(await _services.GetUserByEmailWithDetailsAsync(createUserDTO.Email));
        }
    }
}
