using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GrpcRestGateway", Version = "v1" });
});

// gRPC client services for each gRPC service
builder.Services.AddGrpcClient<Account.Grpc.AccountGrpcService.AccountGrpcServiceClient>(o =>
{
    o.Address = new Uri("http://localhost:5077");
});

builder.Services.AddGrpcClient<Todo.Grpc.TodoListGrpcService.TodoListGrpcServiceClient>(o =>
{
    o.Address = new Uri("http://localhost:5144");
});

builder.Services.AddGrpcClient<Todo.Grpc.TodoItemGrpcService.TodoItemGrpcServiceClient>(o =>
{
    o.Address = new Uri("http://localhost:5144");
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrpcRestGateway v1"));
}

app.MapControllers();
app.Run();
