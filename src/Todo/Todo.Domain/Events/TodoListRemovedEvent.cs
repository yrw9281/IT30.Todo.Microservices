using Common.Library.Seedwork;

namespace Todo.Domain.Events;

public class TodoListRemovedEvent : IDomainEvent
{
    public Guid TodoListId { get; }

    public TodoListRemovedEvent(Guid todoListId)
    {
        TodoListId = todoListId;
    }
}
