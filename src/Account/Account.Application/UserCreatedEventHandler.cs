using Account.Domain.Events;
using Common.Library.IntegrationEvents;
using Common.Library.Services;
using MediatR;

namespace Account.Application;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly RabbitMQService _rabbitMQService;

    public UserCreatedEventHandler(RabbitMQService rabbitMQService)
    {
        _rabbitMQService = rabbitMQService;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserCreatedIntegrationEvent(notification.UserId, DateTime.UtcNow);

        _rabbitMQService.SendMessage(integrationEvent);

        return Task.CompletedTask;
    }
}
