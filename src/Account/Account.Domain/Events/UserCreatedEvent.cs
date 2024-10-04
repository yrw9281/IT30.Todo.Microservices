using Common.Library.Seedwork;

namespace Account.Domain.Events;

public record UserCreatedEvent(Guid UserId) : IDomainEvent;