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

    public async Task<TodoItemResult> CreateTodoItemAsync(Guid userId, string content)
    {
        var item = TodoItem.Create(content, userId);

        _todoItemRepository.Add(item);

        await _todoItemRepository.UnitOfWork.SaveEntitiesAsync();

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public async Task<TodoItemResult> FinishTodoItemAsync(Guid guid)
    {
        var item = _todoItemRepository.GetByGuid(guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");
        if (item.Status != new TodoItemStatus(Domain.ValueObjects.Enums.TodoItemState.Todo))
            throw new ArgumentException("Todo item cannot be finished");

        item.MarkAsFinished();

        await _todoItemRepository.UnitOfWork.SaveEntitiesAsync();

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }

    public async Task<TodoItemResult> RemoveTodoItemAsync(Guid guid)
    {
        var item = _todoItemRepository.GetByGuid(guid);

        if (item == null)
            throw new ArgumentException("Todo item not exists");

        item.Remove();

        await _todoItemRepository.UnitOfWork.SaveEntitiesAsync();

        return new TodoItemResult(item.Id.Value, item.ListId, item.Content, item.Status);
    }
}
