namespace Todo.Application;

public interface ITodoListService
{
    CreateTodoListResult CreateTodoList(Guid userId, string name, string description);
}
