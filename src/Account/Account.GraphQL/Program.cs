using Account.Application;
using Account.GraphQL;
using Account.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAccountApplication().AddAccountInfrastructure();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering();

var app = builder.Build();

app.MapGraphQL();

app.Run();
