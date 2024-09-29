using Common.Library.Seedwork;
using Todo.Domain.ValueObjects;
using Todo.Domain.ValueObjects.Enums;

namespace Todo.Domain.Aggregates;

public class TodoItem : Entity<TodoItemId>, IAggregateRoot
{
    public string Content { get; private set; } = string.Empty;
    public TodoItemStatus Status { get; private set; } = TodoItemStatus.Default();
    public Guid ListId { get; private set; }

    private TodoItem() { }

    public TodoItem(TodoItemId id, string content, Guid listId)
    {
        Id = id;
        Content = content;
        Status = TodoItemStatus.Default();
        ListId = listId;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static TodoItem Create(
        string content,
        Guid listId)
        => new()
        {
            Id = TodoItemId.Create(),
            Content = content,
            Status = TodoItemStatus.Default(),
            ListId = listId,
            CreatedDateTime = DateTime.UtcNow,
            UpdatedDateTime = DateTime.UtcNow
        };

    public void MarkAsFinished()
    {
        Status = new TodoItemStatus(TodoItemState.Finished);
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void Remove()
    {
        Status = new TodoItemStatus(TodoItemState.Removed);
        UpdatedDateTime = DateTime.UtcNow;
    }

}
