using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITeamService
    {
        Task<PagedResultDTO<TeamMiniDTO>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize);
        Task<TeamMiniDTO> CreateTeamAsync(CreateTeamDTO dto);
        Task<List<TeamLookUpDTO>> GetTeamLookUpAsync(string? search, int take);
        Task<TeamDTO> GetTeamByIdAsync(ulong id);
        Task<TeamMiniDTO> UpdateTeamAsync(ulong id, UpdateTeamDTO dto);
        Task<bool> DeleteTeamAsync(ulong id);

    }
}
