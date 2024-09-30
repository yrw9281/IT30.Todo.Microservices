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

    public override Task<CreateTodoListResponse> CreateTodoList(CreateTodoListRequest request, ServerCallContext context)
    {
        var result = _todoListService.CreateTodoList(new Guid(request.UserId), request.Name, request.Description);

        CreateTodoListResponse response = new()
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            Name = result.Name,
            Description = result.Description
        };

        return Task.FromResult(response);
    }
}
