using Account.Application;
using Common.Library.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
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

    public static IServiceCollection AddAccountAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtProvider = new JwtProvider(configuration);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
            .AddJwtBearer(options =>
                options.TokenValidationParameters = jwtProvider.GetTokenValidationParameters());

        return services;
    }
}
