namespace Todo.Application;

public interface ITodoItemService
{
    Task<TodoItemResult> CreateTodoItemAsync(Guid listId, string content);
    Task<TodoItemResult> FinishTodoItemAsync(Guid guid);
    Task<TodoItemResult> RemoveTodoItemAsync(Guid guid);
}
