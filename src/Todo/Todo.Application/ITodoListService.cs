namespace Todo.Application;

public interface ITodoListService
{
    TodoListResult CreateTodoList(Guid userId, string name, string description);
    TodoListResult RemoveTodoList(Guid guid);
}
