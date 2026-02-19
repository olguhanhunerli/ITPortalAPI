using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamMiniDTO> CreateTeamAsync(CreateTeamDTO dto)
        {
            var teamEntity = _mapper.Map<Team>(dto);
            teamEntity.CreatedAt = DateTime.UtcNow;
            await _teamRepository.AddAsync(teamEntity);
            await _teamRepository.SaveChangesAsync();
            return _mapper.Map<TeamMiniDTO>(teamEntity);
        }

        public async Task<bool> DeleteTeamAsync(ulong id)
        {
            var teamEntity = await _teamRepository.GetByIdAsync(id);
            if (teamEntity == null) 
                return false;
            _teamRepository.Remove(teamEntity);
            await _teamRepository.SaveChangesAsync();
            return true;

        }

        public async Task<TeamDTO> GetTeamByIdAsync(ulong id)
        {
            var teamEntity = await _teamRepository.GetByIdAsync(id);
            if (teamEntity == null)
            {
                throw new KeyNotFoundException($"Team with ID {id} not found.");
            }
            return _mapper.Map<TeamDTO>(teamEntity);
        }

        public async Task<List<TeamLookUpDTO>> GetTeamLookUpAsync(string? search, int take)
        {
            return await _teamRepository.GetTeamLookUpAsync(search, take);
        }

        public async Task<PagedResultDTO<TeamMiniDTO>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedTeams = await _teamRepository.GetTeamsWithPaginationAsync(pageNumber, pageSize);
            var mappedTeams = _mapper.Map<List<TeamMiniDTO>>(pagedTeams.Items);

            return new PagedResultDTO<TeamMiniDTO>
            {
                TotalCount = pagedTeams.TotalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = mappedTeams
            };
        }

        public async Task<TeamMiniDTO> UpdateTeamAsync(ulong id, UpdateTeamDTO dto)
        {
            var teamEntity = await _teamRepository.GetByIdAsync(id);
            if (teamEntity == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }
            _teamRepository.Update(teamEntity);
            await _teamRepository.SaveChangesAsync();
            return _mapper.Map<TeamMiniDTO>(teamEntity);
        }
    }
}
