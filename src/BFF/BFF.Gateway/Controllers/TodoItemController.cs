using Todo.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly TodoItemGrpcService.TodoItemGrpcServiceClient _todoItemClient;

    public TodoItemController(TodoItemGrpcService.TodoItemGrpcServiceClient todoItemClient)
    {
        _todoItemClient = todoItemClient;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemRequest request)
    {
        var response = await _todoItemClient.CreateTodoItemAsync(request);
        return Ok(response);
    }

    [HttpPost("finish")]
    public async Task<IActionResult> FinishTodoItem([FromBody] FinishTodoItemRequest request)
    {
        var response = await _todoItemClient.FinishTodoItemAsync(request);
        return Ok(response);
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveTodoItem(Guid id)
    {
        var response = await _todoItemClient.RemoveTodoItemAsync(new RemoveTodoItemRequest() { Id = id.ToString() });
        return Ok(response);
    }
}
