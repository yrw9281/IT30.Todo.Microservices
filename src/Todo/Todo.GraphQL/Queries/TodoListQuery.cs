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
                UpdatedDateTime = todoList.UpdatedDateTime,
                TodoItems = todoItemRepository.GetTodoItems().ToList()
                    .Where(item => item.ListId == todoList.Id.Value) // 對應的 TodoItem
                    .Select(todoItem => new TodoItemDto
                    {
                        Id = todoItem.Id.Value.ToString(),
                        Content = todoItem.Content,
                        State = todoItem.Status.State,
                        Color = todoItem.Status.Color,
                        ListId = todoItem.ListId.ToString(),
                        CreatedDateTime = todoItem.CreatedDateTime,
                        UpdatedDateTime = todoItem.UpdatedDateTime
                    }).ToList() // 將 Items 轉成 List
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

}
