using GrpcService.Protos;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using GrpcService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace GrpcService.Services;

public class GrpcCourseService : CourseService.CourseServiceBase
{
    private readonly CourseDbContext _dbContext;

    public GrpcCourseService(CourseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public override async Task<CourseList> GetCourses(Empty request, ServerCallContext context)
    {
        var courses = await _dbContext.Courses
            .Include(c => c.Sections)
            .ThenInclude(s => s.Lectures)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        var courseList = new CourseList();
        courseList.Courses.AddRange(courses);

        return await Task.FromResult(courseList);
    }

    public override async Task GetCoursesAsStream(Empty request, IServerStreamWriter<Course> responseStream, ServerCallContext context)
    {
        var courses = await _dbContext.Courses
            .Include(c => c.Sections)
            .ThenInclude(s => s.Lectures)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        foreach (var item in courses)
        {
            await responseStream.WriteAsync(item);
        }
    }

    public override async Task<OkResponse> SendCourseAsStream(IAsyncStreamReader<Course> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var course = requestStream.Current;
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        return new OkResponse() { Message = "Created Course Successfully..." };
    }

    public override async Task BiStream(IAsyncStreamReader<Course> requestStream, IServerStreamWriter<CourseId> responseStream, ServerCallContext context)
    {
        await foreach (var course in requestStream.ReadAllAsync())
        {
            try
            {
                if (_dbContext.Courses.Any(c => c.Id == course.Id))
                {
                    throw new RpcException(new Status(StatusCode.AlreadyExists, "The course record already exists."));
                }


                await _dbContext.Courses.AddAsync(course);
                int count = await _dbContext.SaveChangesAsync();

                if (count < 0)
                    throw new RpcException(new Status(StatusCode.Unknown, "Unable to save record in Database"));

                await responseStream.WriteAsync(new CourseId { Id = course.Id.ToString() });
            }
            catch (SqliteException dbEx)
            {
                var metadata = new Metadata
                {
                    {"ErrorCode", dbEx.SqliteErrorCode.ToString() },
                    {"Source", dbEx.Source! }
                };

                throw new RpcException(new Status(StatusCode.Unknown, dbEx.Message), metadata);
            }
        }
    }
}
