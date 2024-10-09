using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GraphQLController : ControllerBase
{
    private readonly IGraphQLClient _graphQLClient;

    public GraphQLController(IGraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }

    // Define a model to accept GraphQL query and variables from the POST body
    public class GraphQLQuery
    {
        public required string Query { get; set; }
        public object? Variables { get; set; } = new { };
    }

    [HttpPost("query")]
    public async Task<IActionResult> ExecuteQueryAsync([FromBody] GraphQLQuery request)
    {
        var graphQLRequest = new GraphQLRequest
        {
            Query = request.Query,
            Variables = request.Variables
        };

        try
        {
            var response = await _graphQLClient.SendQueryAsync<dynamic>(graphQLRequest);

            if (response.Errors != null && response.Errors.Length > 0)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
