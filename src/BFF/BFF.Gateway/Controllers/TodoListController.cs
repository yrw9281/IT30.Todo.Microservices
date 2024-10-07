using Todo.Grpc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BFF.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveTodoList(Guid id)
    {
        var response = await _todoListClient.RemoveTodoListAsync(new RemoveTodoListRequest() { Id = id.ToString() });
        return Ok(response);
    }
}
