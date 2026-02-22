using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team,ulong>
    {
        Task<PagedResultDTO<TeamMiniDTO>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<TeamLookUpDTO>> GetTeamLookUpAsync(string? search, int take);
        Task<Team> GetTeamByIdAsync(ulong id);
        Task<bool> ExistsAsync(ulong id);
        Task<bool> TeamBelongsToDepartmentAsync(ulong teamId, ulong departmentId);

    }
}
