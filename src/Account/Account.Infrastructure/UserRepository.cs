using Account.Application;
using Common.Library.Seedwork;

namespace Account.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AccountContext _accountContext;

    public UserRepository(AccountContext accountContext)
    {
        this._accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
    }

    public IUnitOfWork UnitOfWork => _accountContext;

    public Domain.Aggregates.User? GetUserByEmail(string email)
    {
        return _accountContext.Users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(Domain.Aggregates.User user)
    {
        _accountContext.Users.Add(user);
    }
    
    public IQueryable<Domain.Aggregates.User> GetUsers()
    {
        return _accountContext.Users;
    }
}
