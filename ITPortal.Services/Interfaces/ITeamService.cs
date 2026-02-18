using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface ITeamService
    {
        Task<PagedResultDTO<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize);
    }
}
