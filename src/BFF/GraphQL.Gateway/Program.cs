using HotChocolate.AspNetCore;

const string ACCOUNT = "account";
const string TODO = "todo";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(ACCOUNT, c => c.BaseAddress = new Uri("http://localhost:5068/graphql"));
builder.Services.AddHttpClient(TODO, c => c.BaseAddress = new Uri("http://localhost:5261/graphql"));
builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchema(ACCOUNT, ignoreRootTypes: true)
    .AddRemoteSchema(TODO, ignoreRootTypes: true)
    .AddTypeExtensionsFromFile("./Stitching.graphql");

var app = builder.Build();

app.MapGraphQL();

app.Run();
