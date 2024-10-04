using System.Security.Cryptography;
using System.Text;
using Account.Domain.Events;
using Account.Domain.ValueObjects;
using Common.Library.Seedwork;

namespace Account.Domain.Aggregates;

public class User : Entity<UserId>, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    private User() { }

    public User(UserId id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = HashPassword(password); // Hash
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        var user = new User()
        {
            Id = UserId.Create(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = HashPassword(password), // Hash
            CreatedDateTime = DateTime.UtcNow,
            UpdatedDateTime = DateTime.UtcNow
        };

        user.AddDomainEvent(new UserCreatedEvent(user.Id.Value));

        return user;
    }

    public bool VerifyPassword(string password) => PasswordHash == HashPassword(password); // Hash

    private static string HashPassword(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hashedBytes);
    }
}
