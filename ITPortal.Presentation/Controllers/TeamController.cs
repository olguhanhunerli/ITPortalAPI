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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(ulong id)
        {
            try
            {
                var team = await _service.GetTeamByIdAsync(id);
                return Ok(team);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(ulong id)
        {
            var result = await _service.DeleteTeamAsync(id);
            if (!result)
            {
                return NotFound($"Team with ID {id} not found.");
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(ulong id, [FromBody] UpdateTeamDTO teamDto)
        {
            try
            {
                var updatedTeam = await _service.UpdateTeamAsync(id, teamDto);
                return Ok(updatedTeam);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
