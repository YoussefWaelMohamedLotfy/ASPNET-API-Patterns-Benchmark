using GraphQL;

namespace GraphQL_API.Queries;

public class GraphQLQuery
{
    public string Query { get; set; } = default!;

    public Inputs Variables { get; set; } = default!;

    public string OperationName { get; set; } = default!;
}
