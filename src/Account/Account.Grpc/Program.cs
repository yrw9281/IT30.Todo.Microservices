using Account.Application;
using Account.Grpc.Services;
using Account.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddAccountApplication().AddAccountInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcAccountService>();
app.MapGrpcReflectionService();

app.Run();
