using Todo.Domain.ValueObjects.Enums;

namespace Todo.Application;

public record CreateTodoListResult(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    TodoListStatus Status
);