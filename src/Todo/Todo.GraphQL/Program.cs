using Todo.Application;
using Todo.Infrastructure;
using Todo.GraphQL.Queries;
using HotChocolate.Stitching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTodoApplication().AddTodoInfrastructure();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<TodoListQuery>()
    .AddTypeExtension<TodoItemQuery>()
    .AddFiltering();

var app = builder.Build();

app.MapGraphQL();

app.Run();

