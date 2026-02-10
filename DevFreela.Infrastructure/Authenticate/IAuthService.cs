 using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevFreela.Infrastructure.Authenticate
{
    public interface IAuthService
    {

        string ComputeHash(string password);

        string GenerateToken(string email, string role); 

    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ComputeHash(string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var keySecurity = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(keySecurity, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("username", email),
                new Claim("role", role)
            };

            var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
