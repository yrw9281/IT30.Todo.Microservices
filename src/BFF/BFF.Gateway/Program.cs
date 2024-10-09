using Microsoft.OpenApi.Models;
using Account.Infrastructure;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

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

// Register GraphQLHttpClient for DI
builder.Services.AddSingleton<IGraphQLClient>(s =>
{
    var options = new GraphQLHttpClientOptions
    {
        EndPoint = new Uri("http://localhost:5214/graphql/")
    };

    return new GraphQLHttpClient(options, new SystemTextJsonSerializer());
});

builder.Services.AddAccountAuthentication(builder.Configuration);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrpcRestGateway v1"));
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
