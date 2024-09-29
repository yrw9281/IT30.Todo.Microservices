namespace Account.Application;

public interface IAccountService
{
    AuthenticationResult Register(string FirstName, string lastName, string email, string password);
    AuthenticationResult Login(string email, string password);
    Task<Guid> ValidateTokenAsync(string token);
}
