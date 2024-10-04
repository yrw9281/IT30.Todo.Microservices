using Common.Library.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application;

namespace Todo.Infrastructure;

public static class TodoInfrastructureRegister
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddDbContext<TodoContext>();
        services.AddScoped<DomainEventsInterceptor>();

        return services;
    }
}
