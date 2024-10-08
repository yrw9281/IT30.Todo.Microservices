using Account.Application;
using Account.Domain.ValueObjects;
using Account.GraphQL.Models;

namespace Account.GraphQL;

public class Query
{
    [UseFiltering]
    public IQueryable<UserDto> GetUsers([Service] IUserRepository repository)
        => repository.GetUsers()
            .Select(user => new UserDto
            {
                Id = user.Id.Value.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDateTime = user.CreatedDateTime,
                UpdatedDateTime = user.UpdatedDateTime
            });

    public UserDto? GetUserById([Service] IUserRepository repository, string userId)
        => repository.GetUsers()
            .Where(user => user.Id == UserId.Create(new Guid(userId)))
            .Select(user => new UserDto
            {
                Id = user.Id.Value.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDateTime = user.CreatedDateTime,
                UpdatedDateTime = user.UpdatedDateTime
            }).FirstOrDefault();
}

