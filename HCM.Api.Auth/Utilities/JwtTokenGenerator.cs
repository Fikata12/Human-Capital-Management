using HCM.Data.Models;
using HCM.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HCM.Api.Auth.Utilities
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string jwtSecret;
        private readonly int jwtLifespanMinutes;

        public JwtTokenGenerator(IConfiguration config)
        {
            jwtSecret = config["Jwt:Secret"]!;
            jwtLifespanMinutes = int.Parse(config["Jwt:Lifespan"]!);
        }

        public string GenerateToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtLifespanMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
