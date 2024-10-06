using Common.Library.IntegrationEvents;
using Common.Library.Seedwork;
using Common.Library.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Todo.Application;

public static class TodoApplicationRegister
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoListService, TodoListService>();
        services.AddScoped<ITodoItemService, TodoItemService>();

        services.AddSingleton<RabbitMQService>();
        services.AddScoped<IIntegrationEventHandler<UserCreatedIntegrationEvent>, UserCreatedIntegrationEventHandler>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IApplicationBuilder UseTodoApplication(this IApplicationBuilder app)
    {
        // 使用 IServiceScope 來取得服務的實例
        using (var scope = app.ApplicationServices.CreateScope())
        {
            // 解析取得 RabbitMQService
            var rabbitMQService = scope.ServiceProvider.GetRequiredService<RabbitMQService>();

            // 傳入取得 EventHandlers 的 Delegate
            rabbitMQService.StartListening(sp => sp.GetRequiredService<IIntegrationEventHandler<UserCreatedIntegrationEvent>>());
        }

        return app;
    }
}
