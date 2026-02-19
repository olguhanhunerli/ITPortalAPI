using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.AuthDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }
        private static bool VerifyPassword(string stored, string password)
        {
            var parts = stored.Split('.', 3);
            if (parts.Length != 3) return false;
            if (parts[0] != "HMACSHA256") return false;

            var salt = parts[1];
            var expectedHash = parts[2];

            using var hmac = new System.Security.Cryptography.HMACSHA256(Convert.FromBase64String(salt));
            var computed = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computed == expectedHash;
        }
        public async Task<TokenResponseDTO> LoginAsync(LoginRequestDTO dto, string? userAgent, string? ip)
        {
            var user = await _userRepository.GetUserByEmailWithDetailsAsync(dto.Email);
            if (user == null || user.DeletedAt != null || !user.IsActive)
                throw new UnauthorizedAccessException("Email/şifre hatalı.");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new UnauthorizedAccessException("Bu kullanıcı local girişe uygun değil.");

            if (!VerifyPassword(user.PasswordHash, dto.Password))
                throw new UnauthorizedAccessException("Email/şifre hatalı.");

            var accessToken = _tokenService.GenerateAccessToken(user);

            var refresh = _tokenService.GenerateRefreshToken();
            var refreshHash = _tokenService.HashToken(refresh.RawToken);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAt = refresh.ExpiresAt,
                UserAgent = userAgent,
                IpAddress = ip,
                Revoked = false,
                CreatedAt = DateTime.UtcNow
            });

            await _refreshTokenRepository.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refresh.RawToken,
                RefreshTokenExpiresAt = refresh.ExpiresAt
            };
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var hash = _tokenService.HashToken(refreshToken);
            var existing = await _refreshTokenRepository.GetValidByHashAsync(hash);
            if (existing == null) return; 
            await _refreshTokenRepository.RevokeAsync(existing);
            await _refreshTokenRepository.SaveChangesAsync();

        }

        public async Task<TokenResponseDTO> RefreshAsync(string refreshToken, string? userAgent, string? ip)
        {
            var hash = _tokenService.HashToken(refreshToken);
            var existing = await _refreshTokenRepository.GetValidByHashAsync(hash);
            if (existing == null)
                throw new UnauthorizedAccessException("Geçersiz refresh token.");
            await _refreshTokenRepository.RevokeAsync(existing);

            var user = existing.User;
            if (user == null)
            {
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
            }

            var newAccess = _tokenService.GenerateAccessToken(user);
            var newRefresh = _tokenService.GenerateRefreshToken();
            var newRefreshHash = _tokenService.HashToken(newRefresh.RawToken);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                TokenHash = newRefreshHash,
                ExpiresAt = newRefresh.ExpiresAt,
                UserAgent = userAgent,
                IpAddress = ip,
                Revoked = false,
                CreatedAt = DateTime.UtcNow
            });

            await _refreshTokenRepository.SaveChangesAsync();
            return new TokenResponseDTO {
                AccessToken = newAccess,
                RefreshToken = newRefresh.RawToken,
                RefreshTokenExpiresAt = newRefresh.ExpiresAt
            };
        }
    }
}
