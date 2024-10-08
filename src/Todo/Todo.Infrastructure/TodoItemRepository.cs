using Common.Library.Seedwork;
using Todo.Application;
using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;

namespace Todo.Infrastructure;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoContext _todoContext;

    public TodoItemRepository(TodoContext todoContext)
    {
        this._todoContext = todoContext;
    }

    public IUnitOfWork UnitOfWork => _todoContext;

    public void Add(TodoItem item)
    {
        _todoContext.TodoItems.Add(item);
    }

    public TodoItem? GetByGuid(Guid guid)
    {
        return _todoContext.TodoItems.SingleOrDefault(x => x.Id == TodoItemId.Create(guid));
    }

    public ICollection<TodoItem>? FindByListId(Guid guid)
    {
        return _todoContext.TodoItems.Where(x => x.ListId == guid).ToList();
    }

    public IQueryable<TodoItem> GetTodoItems()
    {
        return _todoContext.TodoItems;
    }
}
