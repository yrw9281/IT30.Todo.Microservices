using Grpc.Core;
using Todo.Application;

namespace Todo.Grpc.Services;

public class GrpcTodoItemService : TodoItemGrpcService.TodoItemGrpcServiceBase
{
    private readonly ITodoItemService _todoItemService;

    public GrpcTodoItemService(ITodoItemService todoItemService)
    {
        this._todoItemService = todoItemService;
    }

    public override Task<TodoItemResponse> CreateTodoItem(CreateTodoItemRequest request, ServerCallContext context)
    {
        var result = _todoItemService.CreateTodoItem(new Guid(request.ListId), request.Content);

        TodoItemResponse response = new()
        {
            Id = result.Id.ToString(),
            ListId = result.ListId.ToString(),
            Content = result.Content,
            Status = result.Status.State.ToString(),
            Color = result.Status.Color
        };
    
        return Task.FromResult(response);
    }

    public override Task<TodoItemResponse> FinishTodoItem(FinishTodoItemRequest request, ServerCallContext context)
    {
        var result = _todoItemService.FinishTodoItem(new Guid(request.Id));

        TodoItemResponse response = new()
        {
            Id = result.Id.ToString(),
            ListId = result.ListId.ToString(),
            Content = result.Content,
            Status = result.Status.State.ToString(),
            Color = result.Status.Color
        };
    
        return Task.FromResult(response);
    }

    public override Task<TodoItemResponse> RemoveTodoItem(RemoveTodoItemRequest request, ServerCallContext context)
    {
        var result = _todoItemService.RemoveTodoItem(new Guid(request.Id));

        TodoItemResponse response = new()
        {
            Id = result.Id.ToString(),
            ListId = result.ListId.ToString(),
            Content = result.Content,
            Status = result.Status.State.ToString(),
            Color = result.Status.Color
        };
    
        return Task.FromResult(response);
    }
}
