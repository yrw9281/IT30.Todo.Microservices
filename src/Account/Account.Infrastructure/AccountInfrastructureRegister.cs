using Account.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure;

public static class AccountInfrastructureRegister
{
    public static IServiceCollection AddAccountInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<ITokenProvider, JwtProvider>();

        return services;
    }
}
