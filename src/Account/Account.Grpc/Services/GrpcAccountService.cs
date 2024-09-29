using Account.Application;
using Grpc.Core;

namespace Account.Grpc.Services;

public class GrpcAccountService : AccountGrpcService.AccountGrpcServiceBase
{
    private readonly IAccountService _accountService;

    public GrpcAccountService(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    public override Task<AuthenticationResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var result = _accountService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        var response = new AuthenticationResponse()
        {
            Id = result.Id.ToString(),
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Token = result.Token
        };

        return Task.FromResult(response);
    }

    public override Task<AuthenticationResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var result = _accountService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse()
        {
            Id = result.Id.ToString(),
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Token = result.Token
        };

        return Task.FromResult(response);
    }

    public override async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, ServerCallContext context)
    {
        var result = await _accountService.ValidateTokenAsync(request.Token);

        return new ValidateTokenResponse() { IsValid = result != Guid.Empty, UserId = result.ToString() };
    }
}
