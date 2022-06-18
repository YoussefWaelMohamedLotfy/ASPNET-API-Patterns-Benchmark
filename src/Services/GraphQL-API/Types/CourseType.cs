using GraphQL.Types;
using GraphQL_API.Models;

namespace GraphQL_API.Types;

public class CourseType : ObjectGraphType<Course>
{
    public CourseType()
    {
        Name = "Course";
        Description = "Represents Course Data";

        Field("id", x => x.Id, type: typeof(IdGraphType), nullable: false).Description("Course ID.");
        Field(x => x.Title, type: typeof(StringGraphType)).Description("Course Title.");
        Field(x => x.Duration, type: typeof(IntGraphType)).Description("Course Duration.");
        Field(x => x.Level);
        Field(x => x.Instructor);
        Field(x => x.PaymentType, type: typeof(PaymentTypeEnum));
        Field<ListGraphType<RatingType>>("Ratings");
    }
}
