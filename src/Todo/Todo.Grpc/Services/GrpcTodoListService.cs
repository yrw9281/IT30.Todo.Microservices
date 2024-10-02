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

    public override async Task<TodoListResponse> CreateTodoList(CreateTodoListRequest request, ServerCallContext context)
    {
        var result = await _todoListService.CreateTodoListAsync(new Guid(request.UserId), request.Name, request.Description);

        TodoListResponse response = new()
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            Name = result.Name,
            Description = result.Description,
            Status = result.Status.ToString()
        };

        return response;
    }

    public override async Task<TodoListResponse> RemoveTodoList(RemoveTodoListRequest request, ServerCallContext context)
    {
        var result = await _todoListService.RemoveTodoListAsync(new Guid(request.Id));

        TodoListResponse response = new()
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            Name = result.Name,
            Description = result.Description,
            Status = result.Status.ToString()
        };

        return response;
    }
}
