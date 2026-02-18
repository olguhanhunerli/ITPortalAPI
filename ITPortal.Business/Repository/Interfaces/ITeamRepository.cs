using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team,ulong>
    {
        Task<PagedResultDTO<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<TeamLookUpDTO>> GetTeamLookUpAsync(string? search, int take);

    }
}
