using Common.Library.Seedwork;
using Todo.Domain.Aggregates;

namespace Todo.Application;

public interface ITodoListRepository : IRepository<TodoList>
{
    void Add(TodoList list);
    TodoList? GetByGuid(Guid guid);
}
