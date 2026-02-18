using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IUserServices
    {
        Task<PagedResultDTO<UserMiniDTO>> GetUsersWithPaginationAsync(int pageNumber, int pageSize);

    }
}
