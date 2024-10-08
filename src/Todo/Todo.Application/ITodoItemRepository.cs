using Common.Library.Seedwork;
using Todo.Domain.Aggregates;

namespace Todo.Application;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    void Add(TodoItem item);
    TodoItem? GetByGuid(Guid guid);
    ICollection<TodoItem>? FindByListId(Guid guid);
    IQueryable<TodoItem> GetTodoItems();
}
