
using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;

namespace Todo.Application;

public class TodoItemService : ITodoItemService
{
    private readonly List<TodoItem> _todoItems = new();
    
    public TodoItemResult CreateTodoItem(Guid userId, string content)
    {
        var item = TodoItem.Create(content, userId);

        _todoItems.Add(item);

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public TodoItemResult FinishTodoItem(Guid guid)
    {
        var item = _todoItems.SingleOrDefault(i => i.Id.Value == guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");
        if (item.Status != new TodoItemStatus(Domain.ValueObjects.Enums.TodoItemState.Todo))
            throw new ArgumentException("Todo item cannot be finished");

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public TodoItemResult RemoveTodoItem(Guid guid)
    {
        var item = _todoItems.SingleOrDefault(i => i.Id.Value == guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }
}
