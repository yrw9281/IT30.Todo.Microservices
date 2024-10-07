using Todo.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly TodoListGrpcService.TodoListGrpcServiceClient _todoListClient;

    public TodoListController(TodoListGrpcService.TodoListGrpcServiceClient todoListClient)
    {
        _todoListClient = todoListClient;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTodoList([FromBody] CreateTodoListRequest request)
    {
        var response = await _todoListClient.CreateTodoListAsync(request);
        return Ok(response);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveTodoList([FromBody] RemoveTodoListRequest request)
    {
        var response = await _todoListClient.RemoveTodoListAsync(request);
        return Ok(response);
    }
}
