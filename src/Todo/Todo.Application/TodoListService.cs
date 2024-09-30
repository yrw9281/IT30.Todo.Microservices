
using Todo.Domain.Aggregates;

namespace Todo.Application;

public class TodoListService : ITodoListService
{
    private readonly List<TodoList> _todoLists = new();
    
    public CreateTodoListResult CreateTodoList(Guid userId, string name, string description)
    {
        var list = TodoList.Create(name, description, userId);

        _todoLists.Add(list);

        return new CreateTodoListResult(list.Id.Value, list.UserId, list.Name, list.Description, list.Status);
    }
}
