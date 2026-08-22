using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarRent.Application.Abstractions;
using CarRent.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace CarRent.Infrastructure.Authentication
{
   public class JwtService :IJwtService
    {
        private readonly IConfiguration _configuration;
        
      public JwtService (IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public string GenerateToken(User user)
        {
            var secretKey = _configuration["JwtSettings:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credits= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
             new Claim(ClaimTypes.Role, user.UserRole.ToString())
             };

            var token = new JwtSecurityToken(
              issuer: _configuration["JwtSettings:Issuer"],
              audience: _configuration["JwtSettings:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddHours(2),
              signingCredentials: credits
          );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
