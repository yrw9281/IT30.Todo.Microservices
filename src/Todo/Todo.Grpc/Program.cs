using Todo.Grpc.Services;
using Todo.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddTodoApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcTodoListService>();
app.MapGrpcReflectionService();

app.Run();
