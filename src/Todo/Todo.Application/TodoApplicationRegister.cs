using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application;

public static class TodoApplicationRegister
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoListService, TodoListService>();

        return services;
    }

}
