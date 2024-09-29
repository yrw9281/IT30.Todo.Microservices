using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class AccountApplicationRegister
{
    public static IServiceCollection AddAccountApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
