using GrpcClient;

Console.WriteLine("Hello, gRPC World!");

GrpcCourseClient client = new();
client.GetCourses();