using Todo.Application;
using Todo.Domain.ValueObjects;
using Todo.GraphQL.Models;

namespace Todo.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class TodoItemQuery
{
    [UseFiltering]
    public IQueryable<TodoItemDto> GetTodoItems([Service] ITodoItemRepository todoItemRepository)
        => todoItemRepository.GetTodoItems()
            .Select(todoItem => new TodoItemDto
            {
                Id = todoItem.Id.Value.ToString(),
                Content = todoItem.Content,
                State = todoItem.Status.State,
                Color = todoItem.Status.Color,
                ListId = todoItem.ListId.ToString(),
                CreatedDateTime = todoItem.CreatedDateTime,
                UpdatedDateTime = todoItem.UpdatedDateTime
            });

    public TodoItemDto? GetTodoItemById([Service] ITodoItemRepository todoItemRepository, string itemId)
        => todoItemRepository.GetTodoItems()
            .Where(item => item.Id == TodoItemId.Create(new Guid(itemId)))
            .Select(todoItem => new TodoItemDto
            {
                Id = todoItem.Id.Value.ToString(),
                Content = todoItem.Content,
                State = todoItem.Status.State,
                Color = todoItem.Status.Color,
                ListId = todoItem.ListId.ToString(),
                CreatedDateTime = todoItem.CreatedDateTime,
                UpdatedDateTime = todoItem.UpdatedDateTime
            }).FirstOrDefault();
}
