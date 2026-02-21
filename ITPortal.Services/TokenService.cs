using ITPortal.Entities.DTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        new Claim("userId", user.Id.ToString()),
        new Claim("userName", user.Username ?? string.Empty),
        new Claim("fullName", user.FullName ?? string.Empty),
    };

            if (user.DepartmentId.HasValue)
                claims.Add(new Claim("departmentId", user.DepartmentId.Value.ToString()));

            if (user.TeamId.HasValue)
                claims.Add(new Claim("teamId", user.TeamId.Value.ToString()));

            if (user.LocationId.HasValue)
                claims.Add(new Claim("locationId", user.LocationId.Value.ToString()));

            if (user.UserRoles?.Any() == true)
            {
                claims.AddRange(
                    user.UserRoles
                        .Where(ur => ur.Role?.Name != null)
                        .Select(ur => new Claim(ClaimTypes.Role, ur.Role!.Name))
                );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        

        public RefreshTokenDTO GenerateRefreshToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var token = Convert.ToBase64String(bytes);

            return new RefreshTokenDTO
            {
                RawToken = token,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public string HashToken(string token)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}
