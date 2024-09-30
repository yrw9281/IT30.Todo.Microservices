using Grpc.Core;
using Todo.Application;

namespace Todo.Grpc.Services;

public class GrpcTodoListService : TodoListGrpcService.TodoListGrpcServiceBase
{
    private readonly ITodoListService _todoListService;

    public GrpcTodoListService(ITodoListService todoListService)
    {
        this._todoListService = todoListService;
    }

    public override Task<TodoListResponse> CreateTodoList(CreateTodoListRequest request, ServerCallContext context)
    {
        var result = _todoListService.CreateTodoList(new Guid(request.UserId), request.Name, request.Description);

        TodoListResponse response = new()
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            Name = result.Name,
            Description = result.Description,
            Status = result.Status.ToString()
        };

        return Task.FromResult(response);
    }
}
