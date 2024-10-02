using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;

namespace Todo.Application;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemService(ITodoItemRepository todoItemRepository)
{
        this._todoItemRepository = todoItemRepository;
    }

    public TodoItemResult CreateTodoItem(Guid userId, string content)
    {
        var item = TodoItem.Create(content, userId);

        _todoItemRepository.Add(item);

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public TodoItemResult FinishTodoItem(Guid guid)
    {
        var item = _todoItemRepository.GetByGuid(guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");
        if (item.Status != new TodoItemStatus(Domain.ValueObjects.Enums.TodoItemState.Todo))
            throw new ArgumentException("Todo item cannot be finished");

        item.MarkAsFinished();
        
        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public TodoItemResult RemoveTodoItem(Guid guid)
    {
        var item = _todoItemRepository.GetByGuid(guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");

        item.Remove();
        
        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }
}
