using GraphQL.Types;
using GraphQL_API.Models;

namespace GraphQL_API.Types;

public class RatingType : ObjectGraphType<Rating>
{
    public RatingType()
    {
        Name = "Rating";

        Field(x => x.Id, type: typeof(IdGraphType), nullable: false);
        Field(x => x.CourseId);
        Field(x => x.StudentName);
        Field(x => x.Review);
        Field(x => x.StarValue);
    }
}
