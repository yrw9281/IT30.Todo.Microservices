namespace Todo.Application;

public interface ITodoListService
{
    Task<TodoListResult> CreateTodoListAsync(Guid userId, string name, string description);
    Task<TodoListResult> RemoveTodoListAsync(Guid guid);
}
