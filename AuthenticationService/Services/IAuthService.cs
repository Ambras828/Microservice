namespace AuthenticationService.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username);
        bool ValidateUser(string username, string password);
    }
}
