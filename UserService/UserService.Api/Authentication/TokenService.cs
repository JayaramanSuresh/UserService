using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Api.Interface;

namespace UserService.Api.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) => _config = config;

        public string BuildToken(string userId, string email)
        {
            var jwtKey = _config["JwtSettings:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "JWT key cannot be null or empty.");
            }

            var durationInMinutesString = _config["JwtSettings:DurationInMinutes"];
            if (string.IsNullOrEmpty(durationInMinutesString))
            {
                throw new ArgumentNullException(nameof(durationInMinutesString), "JWT duration cannot be null or empty.");
            }

            if (!double.TryParse(durationInMinutesString, out var durationInMinutes))
            {
                throw new FormatException("JWT duration is not a valid number.");
            }

            var claims = new[]
            {
                       new Claim(JwtRegisteredClaimNames.Sub, userId),
                       new Claim(JwtRegisteredClaimNames.Email, email),
                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
