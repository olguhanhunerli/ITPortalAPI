using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model.Common;

namespace ITPortal.Services.Interfaces
{
    public interface IUserServices
    {
        Task<PagedResultDTO<UserMiniDTO>> GetUsersWithPaginationAsync(int pageNumber, int pageSize);
        Task<UserDTO> GetUserByEmailWithDetailsAsync(string email);
        Task<List<UserLookUpDTO>> GetUserLookUpAsync(string? search, int take);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);

    }
}
