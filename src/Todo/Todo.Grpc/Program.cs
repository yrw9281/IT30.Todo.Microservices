using Todo.Grpc.Services;
using Todo.Application;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddTodoApplication().AddTodoInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcTodoListService>();
app.MapGrpcService<GrpcTodoItemService>();
app.MapGrpcReflectionService();

app.Run();
