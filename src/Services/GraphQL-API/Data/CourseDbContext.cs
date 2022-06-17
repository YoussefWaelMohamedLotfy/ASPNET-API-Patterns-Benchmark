using GraphQL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_API.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<Section> Sections { get; set; } = default!;
    public DbSet<Rating> Ratings { get; set; } = default!;
    public DbSet<Lecture> Lectures { get; set; } = default!;
}
