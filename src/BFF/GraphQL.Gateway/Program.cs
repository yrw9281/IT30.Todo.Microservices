using HotChocolate.AspNetCore;

const string ACCOUNT = "account";
const string TODO = "todo";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHttpClient(ACCOUNT, c => c.BaseAddress = new Uri(configuration["Services:Account"]!));
builder.Services.AddHttpClient(TODO, c => c.BaseAddress = new Uri(configuration["Services:Todo"]!));
builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchema(ACCOUNT, ignoreRootTypes: true)
    .AddRemoteSchema(TODO, ignoreRootTypes: true)
    .AddTypeExtensionsFromFile("./Stitching.graphql");

var app = builder.Build();

app.MapGraphQL();

app.Run();
