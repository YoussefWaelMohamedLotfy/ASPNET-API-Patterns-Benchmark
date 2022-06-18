using GraphQL;
using GraphQL_API.Models;

namespace GraphQL_API.Queries;

public class Query
{
    [GraphQLMetadata("courses")]
    public List<Course> GetCourses()
    {
        var courses = new List<Course>
        {
            new Course
            {
                Id = 1,
                Title = "Fastest Microservices",
                Duration = 120,
                Level = "All",
                Instructor = "SeedACloud",
                PaymentType = PaymentType.PAID,
                Ratings = GetRating(1)
            },
            new Course
            {
                Id = 2,
                Title = "Software Architecture & Design Essentials",
                Duration = 320,
                Level = "Beginner",
                Instructor = "Sree",
                PaymentType = PaymentType.PAID,
                Ratings=GetRating(2)
            }
        };

        return courses;
    }

    [GraphQLMetadata("course")]
    public Course GetSingleCourse(IResolveFieldContext context)
    {
        var courseId = context.GetArgument<int>("id");

        var courses = new List<Course>
        {
            new Course
            {
                Id = 1,
                Title = "Fastest Microservices",
                Duration = 120,
                Level = "All",
                Instructor = "SeedACloud",
                PaymentType = PaymentType.PAID,
            },
            new Course
            {
                Id = 2,
                Title = "Software Architecture & Design Essentials",
                Duration = 320,
                Level = "Beginner",
                Instructor = "Sree",
                PaymentType = PaymentType.PAID,
            }
        };

        return courses.Where(c => c.Id == courseId).FirstOrDefault()!;
    }

    public List<Rating> GetRating(int courseId)
    {
        var ratings = new List<Rating>
        {
            new Rating
            {
                StudentName = "Shree",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 1
            },
            new Rating
            {
                StudentName = "Mark",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 1
            },
            new Rating
            {
                StudentName = "Shree",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 1
            },
            new Rating
            {
                StudentName = "Mark",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 2
            },
            new Rating
            {
                StudentName = "Mark",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 3
            },
            new Rating
            {
                StudentName = "Shree",
                StarValue = 5,
                Review = "Very Useful",
                CourseId = 3
            }
        };

        return ratings.Where(r => r.CourseId == courseId).ToList();
    }

}
