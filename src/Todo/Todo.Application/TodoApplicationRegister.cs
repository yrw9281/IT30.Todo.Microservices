using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Todo.Application;

public static class TodoApplicationRegister
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoListService, TodoListService>();
        services.AddScoped<ITodoItemService, TodoItemService>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
