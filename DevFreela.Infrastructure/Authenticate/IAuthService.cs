using Microsoft.Extensions.Configuration;

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
            throw new NotImplementedException();
        }
    }

}
