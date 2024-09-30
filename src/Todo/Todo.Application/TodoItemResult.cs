using Todo.Domain.ValueObjects;

namespace Todo.Application;

public record TodoItemResult(
    Guid Id,
    Guid ListId,
    string Content,
    TodoItemStatus Status
);