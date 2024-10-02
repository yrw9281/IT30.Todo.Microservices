using Common.Library.Seedwork;
using Todo.Application;
using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;

namespace Todo.Infrastructure;

public class TodoListRepository : ITodoListRepository
{
    private readonly TodoContext _todoContext;

    public TodoListRepository(TodoContext todoContext)
    {
        this._todoContext = todoContext;
    }
    
    public IUnitOfWork UnitOfWork => _todoContext;

    public void Add(TodoList list)
    {
        _todoContext.TodoLists.Add(list);
    }

    public TodoList? GetByGuid(Guid guid)
    {
        return _todoContext.TodoLists.SingleOrDefault(x => x.Id == TodoListId.Create(guid));
    }
}
