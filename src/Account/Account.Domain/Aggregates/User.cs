using Account.Domain.ValueObjects;
using Common.Library.Seedwork;

namespace Account.Domain.Aggregates;

public class User : Entity<UserId>, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User() { }

    public User(UserId id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
        => new()
        {
            Id = UserId.Create(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            CreatedDateTime = DateTime.UtcNow,
            UpdatedDateTime = DateTime.UtcNow
        };

    public bool VerifyPassword(string password) => Password == password;

}
