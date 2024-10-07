using Account.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly AccountGrpcService.AccountGrpcServiceClient _accountClient;

    public AccountController(AccountGrpcService.AccountGrpcServiceClient accountClient)
    {
        _accountClient = accountClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _accountClient.RegisterAsync(request);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _accountClient.LoginAsync(request);
        return Ok(response);
    }
}
