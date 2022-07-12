using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcService.Protos;

namespace GrpcClient;

public class GrpcCourseClient
{
    private readonly GrpcChannel channel;
    private readonly CourseService.CourseServiceClient client;

    public GrpcCourseClient()
    {
        channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { });
        client = new CourseService.CourseServiceClient(channel);
    }

    public void GetCourses()
    {
        Thread.Sleep(2000);
        var courseList = client.GetCourses(new Empty());

        courseList.Courses.ToList().ForEach(c => Console.WriteLine($"ID: {c.Id} - Title: {c.Title}"));
    }
}
