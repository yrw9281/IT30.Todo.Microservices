using MediatR;
using Todo.Domain.Events;

namespace Todo.Application;

public class TodoListRemovedEventHandler : INotificationHandler<TodoListRemovedEvent>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoListRemovedEventHandler(ITodoItemRepository todoItemRepository)
    {
        this._todoItemRepository = todoItemRepository;
    }

    public async Task Handle(TodoListRemovedEvent notification, CancellationToken cancellationToken)
    {
        var items = _todoItemRepository.FindByListId(notification.TodoListId);

        if (items == null) return;

        foreach (var item in items)
        {
            item.Remove();
        }

        await _todoItemRepository.UnitOfWork.SaveEntitiesAsync();
    }   
}