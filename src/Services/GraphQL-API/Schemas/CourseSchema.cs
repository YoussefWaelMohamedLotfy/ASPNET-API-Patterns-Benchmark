using GraphQL.Types;
using GraphQL_API.Queries;

namespace GraphQL_API.Schemas;

public class CourseSchema : Schema
{
    public CourseSchema(ProQuery proQuery, IServiceProvider services) : base(services)
    {
        Query = proQuery;
    }
}
