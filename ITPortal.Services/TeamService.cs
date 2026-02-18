using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
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

        public async Task<PagedResultDTO<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedTeams = await _teamRepository.GetTeamsWithPaginationAsync(pageNumber, pageSize);
            var mappedTeams = _mapper.Map<List<Team>>(pagedTeams.Items);

            return new PagedResultDTO<Team>
            {
                TotalCount = pagedTeams.TotalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = mappedTeams
            };
        }
    }
}
