using ITPortal.Entities.DTOs.TeamDTOs;
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
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetallTeams(int pageNumber, int pageSize)
        {
            var teams = await _service.GetTeamsWithPaginationAsync(pageNumber, pageSize);
            return Ok(teams);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetTeamLookup(string? search, int take = 50)
        {
            var teamLookup = await _service.GetTeamLookUpAsync(search, take);
            return Ok(teamLookup);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDTO teamDto)
        {
            var createdTeam = await _service.CreateTeamAsync(teamDto);
            return Ok(createdTeam);
        }
    }
}
