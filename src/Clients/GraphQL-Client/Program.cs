using GraphQL_Client;

Console.WriteLine("Welcome to the GraphQL Client Demo!");
Console.WriteLine("Click Enter to run the query ...");
Console.ReadLine();

await GraphQLCourseClient.GetCoursesViaPost();
