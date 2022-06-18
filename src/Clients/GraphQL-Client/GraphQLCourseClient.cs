using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL_Client.Models;

namespace GraphQL_Client;

internal class GraphQLCourseClient
{
    public static async Task GetCoursesViaGet()
    {
        var graphqlClient = new GraphQLHttpClient(
            new Uri("https://localhost:5005/graphql/getcourses"), new SystemTextJsonSerializer());

        var queryString = "{ courses { title, level, instructor, ratings { studentName, review } } }";

        // Single Course Query String
        //var queryString = "{ course (id:1) { title, level, instructor } }";

        var response = await graphqlClient.HttpClient.GetAsync($"http://localhost:5006/graphql/getcourses?query={queryString}");
        var result = response.Content.ReadAsStringAsync();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(result.Result);
    }

    public static async Task GetCoursesViaPost()
    {
        var graphqlClient = new GraphQLHttpClient(
            new Uri("https://localhost:5005/graphql/getcourses"), new SystemTextJsonSerializer());

        var queryString = "{ courses { title, level, instructor, ratings { studentName, review } } }";

        var postRequest = new GraphQLRequest
        {
            Query = queryString,
        };

        var response = await graphqlClient.SendQueryAsync<CourseResponse>(postRequest);
        var courseList = response.Data.Courses;
    }

}
