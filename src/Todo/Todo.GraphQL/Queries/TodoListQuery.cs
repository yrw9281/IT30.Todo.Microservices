using Todo.Application;
using Todo.Domain.ValueObjects;
using Todo.GraphQL.Models;

namespace Todo.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class TodoListQuery
{
    [UseFiltering]
    public IQueryable<TodoListDto> GetTodoLists([Service] ITodoListRepository todoListRepository, [Service] ITodoItemRepository todoItemRepository)
        => todoListRepository.GetTodoLists()
            .Select(todoList => new TodoListDto
            {
                Id = todoList.Id.Value.ToString(),
                Name = todoList.Name,
                Description = todoList.Description,
                Status = todoList.Status,
                UserId = todoList.UserId.ToString(),
                CreatedDateTime = todoList.CreatedDateTime,
                UpdatedDateTime = todoList.UpdatedDateTime
            });

    public TodoListDto? GetTodoListById([Service] ITodoListRepository todoListRepository, string listId)
        => todoListRepository.GetTodoLists()
            .Where(list => list.Id == TodoListId.Create(new Guid(listId)))
            .Select(todoList => new TodoListDto
            {
                Id = todoList.Id.Value.ToString(),
                Name = todoList.Name,
                Description = todoList.Description,
                Status = todoList.Status,
                UserId = todoList.UserId.ToString(),
                CreatedDateTime = todoList.CreatedDateTime,
                UpdatedDateTime = todoList.UpdatedDateTime
            }).FirstOrDefault();

    public List<TodoListDto>? GetTodoListsByUserId([Service] ITodoListRepository todoListRepository, string userId)
        => todoListRepository.GetTodoLists()
            .Where(list => list.UserId == new Guid(userId))
            .Select(todoList => new TodoListDto
            {
                Id = todoList.Id.Value.ToString(),
                Name = todoList.Name,
                Description = todoList.Description,
                Status = todoList.Status,
                UserId = todoList.UserId.ToString(),
                CreatedDateTime = todoList.CreatedDateTime,
                UpdatedDateTime = todoList.UpdatedDateTime
            }).ToList();
}
