using Account.Application;
using Common.Library.Seedwork;

namespace Account.Infrastructure;

public class UserRepository : IUserRepository
{
    private static readonly List<Domain.Aggregates.User> _users = new();

    public IUnitOfWork UnitOfWork { get; }

    public Domain.Aggregates.User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(Domain.Aggregates.User user)
    {
        _users.Add(user);
    }
}
