using Todo.Domain.ValueObjects.Enums;

namespace Todo.GraphQL.Models;

public class TodoItemDto
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public TodoItemState? State { get; set; }
    public string? Color { get; set; }
    public string? ListId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public TodoListDto? TodoList { get; set; }
}
