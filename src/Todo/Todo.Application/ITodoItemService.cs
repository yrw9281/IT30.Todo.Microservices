namespace Todo.Application;

public interface ITodoItemService
{
    TodoItemResult CreateTodoItem(Guid listId, string content);
    TodoItemResult FinishTodoItem(Guid guid);
    TodoItemResult RemoveTodoItem(Guid guid);
}
