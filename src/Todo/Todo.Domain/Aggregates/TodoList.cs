using Common.Library.Seedwork;
using Todo.Domain.Events;
using Todo.Domain.ValueObjects;
using Todo.Domain.ValueObjects.Enums;

namespace Todo.Domain.Aggregates;

public class TodoList : Entity<TodoListId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TodoListStatus Status { get; set; }
    public Guid UserId { get; private set; }
    public ICollection<Guid> TodoItemIds { get; set; } = new List<Guid>();

    private TodoList() { }

    public TodoList(TodoListId id, string name, string description, Guid userId)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = TodoListStatus.Active;
        UserId = userId;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static TodoList Create(
    string name,
    string description,
    Guid userId)
    => new()
    {
        Id = TodoListId.Create(),
        Name = name,
        Description = description,
        Status = TodoListStatus.Active,
        UserId = userId,
        CreatedDateTime = DateTime.UtcNow,
        UpdatedDateTime = DateTime.UtcNow
    };

    public void Remove()
    {
        Status = TodoListStatus.Removed;
        UpdatedDateTime = DateTime.UtcNow;
        AddDomainEvent(new TodoListRemovedEvent(this.Id.Value));
    }
}
