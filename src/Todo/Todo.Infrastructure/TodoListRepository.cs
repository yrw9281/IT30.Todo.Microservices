using Common.Library.Seedwork;
using Todo.Application;
using Todo.Domain.Aggregates;

namespace Todo.Infrastructure;

public class TodoListRepository : ITodoListRepository
{
    private static readonly List<TodoList> todoLists = new List<TodoList>();

    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public void Add(TodoList list)
    {
        todoLists.Add(list);
    }

    public TodoList? GetByGuid(Guid guid)
    {
        return todoLists.SingleOrDefault(x => x.Id.Value == guid);
    }
}
