namespace Account.Application;

public interface ITokenProvider
{
    string GenerateToken(Guid userId, string firstName, string lastName);
    Task<string?> ValidateTokenAsync(string token);
}
