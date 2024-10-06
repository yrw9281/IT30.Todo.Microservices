using Common.Library.IntegrationEvents;
using Common.Library.Seedwork;
using Todo.Domain.Aggregates;

namespace Todo.Application;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    private readonly string DEFAULT_LIST_NAME = "Default";
    private readonly string DEFAULT_LIST_DESCRIPTION = "Default";
    private readonly ITodoListRepository _todoListRepository;

    public UserCreatedIntegrationEventHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
    {
        Console.WriteLine($"UserCreatedIntegrationEvent: {integrationEvent.UserId} at {integrationEvent.CreatedDateTime}");

        _todoListRepository.Add(TodoList.Create(DEFAULT_LIST_NAME, DEFAULT_LIST_DESCRIPTION, integrationEvent.UserId));

        await _todoListRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
