using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lost_Found.Models;
using Microsoft.IdentityModel.Tokens;

namespace Lost_Found.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Korisnik korisnik)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"];
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException("Jwt:Key is not configured.");
            }

            var role = korisnik is Admin ? "Admin" : "StandardniKorisnik";

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, korisnik.KorisnikId.ToString()),
                new(ClaimTypes.Name, korisnik.KorisnickoIme),
                new(ClaimTypes.Email, korisnik.Email),
                new(ClaimTypes.Role, role)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expiresInMinutes = jwtSection.GetValue<int?>("ExpiresInMinutes") ?? 120;

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
