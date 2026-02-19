using ITPortal.Entities.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(LoginRequestDTO dto, string? userAgent, string? ip);
        Task<TokenResponseDTO> RefreshAsync(string refreshToken, string? userAgent, string? ip);
        Task LogoutAsync(string refreshToken);
    }
}
