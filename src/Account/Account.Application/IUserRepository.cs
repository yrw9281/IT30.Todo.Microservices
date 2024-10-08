using Account.Domain.Aggregates;
using Common.Library.Seedwork;

namespace Account.Application;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByEmail(string email);
    void Add(User user);
    IQueryable<User> GetUsers();
}