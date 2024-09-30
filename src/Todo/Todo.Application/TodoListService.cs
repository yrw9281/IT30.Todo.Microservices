
using Todo.Domain.Aggregates;

namespace Todo.Application;

public class TodoListService : ITodoListService
{
    private readonly List<TodoList> _todoLists = new();
    
    public TodoListResult CreateTodoList(Guid userId, string name, string description)
    {
        var list = TodoList.Create(name, description, userId);

        _todoLists.Add(list);

        return new TodoListResult(list.Id.Value, list.UserId, list.Name, list.Description, list.Status);
    }
}
