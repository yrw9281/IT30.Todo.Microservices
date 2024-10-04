using System.Text.Json;
using Account.Domain.Events;
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
        var message = JsonSerializer.Serialize(notification);

        _rabbitMQService.SendMessage(typeof(UserCreatedEvent).Name, message);

        return Task.CompletedTask;
    }
}
