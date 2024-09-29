using Account.Domain.Aggregates;

namespace Account.Application;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;

    public AccountService(IUserRepository userRepository, ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
    }

    public AuthenticationResult Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user == null)
            throw new ArgumentException("Email address not exists");
        if (!user.VerifyPassword(password))
            throw new ArgumentException("Permission denied");

        return new AuthenticationResult(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            _tokenProvider.GenerateToken(user.Id.Value, user.FirstName, user.LastName)
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
            throw new ArgumentException("Email address already exists");

        var user = User.Create(firstName, lastName, email, password);

        _userRepository.Add(user);

        _userRepository.UnitOfWork.SaveEntitiesAsync().Wait();

        return new AuthenticationResult(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            _tokenProvider.GenerateToken(user.Id.Value, user.FirstName, user.LastName)
        );
    }

    public async Task<Guid> ValidateTokenAsync(string token)
    {
        var guid = await _tokenProvider.ValidateTokenAsync(token);

        Guid.TryParse(guid, out Guid result);

        return result;
    }
}
