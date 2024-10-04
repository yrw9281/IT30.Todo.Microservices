using System.Reflection;
using Common.Library.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class AccountApplicationRegister
{
    public static IServiceCollection AddAccountApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<RabbitMQService>();

        return services;
    }
}
