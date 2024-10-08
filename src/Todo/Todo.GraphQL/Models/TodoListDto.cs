using Todo.Domain.ValueObjects.Enums;

namespace Todo.GraphQL.Models;

public class TodoListDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TodoListStatus? Status { get; set; }
    public string? UserId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public List<TodoItemDto>? TodoItems { get; set; }
}
