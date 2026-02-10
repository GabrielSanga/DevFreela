namespace DevFreela.Infrastructure.Authenticate
{
    public interface IAuthService
    {

        string ComputeHash(string password);

        string GenerateToken(string email, string role);

    }

}
