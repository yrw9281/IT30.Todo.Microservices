using Common.Library.Seedwork;

namespace Common.Library.IntegrationEvents;

public record UserCreatedIntegrationEvent(Guid UserId, DateTime CreatedDateTime) : IIntegrationEvent;
