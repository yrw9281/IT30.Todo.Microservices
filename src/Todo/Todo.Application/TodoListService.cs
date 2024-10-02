using Todo.Domain.Aggregates;

namespace Todo.Application;

public class TodoListService : ITodoListService
{
    private readonly ITodoListRepository _todoListRepository;

    public TodoListService(ITodoListRepository todoListRepository)
    {
        this._todoListRepository = todoListRepository;
    }

    public async Task<TodoListResult> CreateTodoListAsync(Guid userId, string name, string description)
    {
        var list = TodoList.Create(name, description, userId);

        _todoListRepository.Add(list);

        await _todoListRepository.UnitOfWork.SaveEntitiesAsync();

        return new TodoListResult(list.Id.Value, list.UserId, list.Name, list.Description, list.Status);
    }

    public async Task<TodoListResult> RemoveTodoListAsync(Guid guid)
    {
        var list = _todoListRepository.GetByGuid(guid);

        if (list == null)
            throw new ArgumentException("Todo list not exists");

        list.Remove();

        await _todoListRepository.UnitOfWork.SaveEntitiesAsync();

        return new TodoListResult(list.Id.Value, list.UserId, list.Name, list.Description, list.Status);
    }
}
