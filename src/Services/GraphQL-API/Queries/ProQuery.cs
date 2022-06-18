using GraphQL.Types;
using GraphQL_API.Data;
using GraphQL_API.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_API.Queries;

public class ProQuery : ObjectGraphType
{
    public ProQuery(CourseDbContext dbContext)
    {
        FieldAsync<ListGraphType<CourseType>>("courses",
            resolve: async context =>
            {
                var courses = await dbContext.Courses
                    .Include(c => c.Ratings)
                    .AsNoTracking()
                    .ToListAsync();

                return courses;
            });
    }
}
