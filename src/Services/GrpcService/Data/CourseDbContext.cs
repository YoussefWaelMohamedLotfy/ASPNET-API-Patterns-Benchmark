using GrpcService.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<Course.Types.Section> Sections { get; set; } = default!;
    public DbSet<Rating> Ratings { get; set; } = default!;
    public DbSet<Course.Types.Section.Types.Lecture> Lectures { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course.Types.Section.Types.Lecture>().OwnsOne(l => l.Subject);
        modelBuilder.Entity<Course.Types.Section.Types.Lecture>().OwnsOne(l => l.Assignment);

        base.OnModelCreating(modelBuilder);
    }
}
