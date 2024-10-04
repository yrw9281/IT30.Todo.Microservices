using Account.Application;
using Common.Library.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure;

public static class AccountInfrastructureRegister
{
    public static IServiceCollection AddAccountInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<ITokenProvider, JwtProvider>();
        services.AddDbContext<AccountContext>();        
        services.AddScoped<DomainEventsInterceptor>();

        return services;
    }
}
