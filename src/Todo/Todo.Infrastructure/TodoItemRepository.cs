using Common.Library.Seedwork;
using Todo.Application;
using Todo.Domain.Aggregates;

namespace Todo.Infrastructure;

public class TodoItemRepository : ITodoItemRepository
{
    private static readonly List<TodoItem> TodoItems = new List<TodoItem>();

    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public void Add(TodoItem item)
    {
        TodoItems.Add(item);
    }

    public TodoItem? GetByGuid(Guid guid)
    {
        return TodoItems.SingleOrDefault(x => x.Id.Value == guid);
    }
}
