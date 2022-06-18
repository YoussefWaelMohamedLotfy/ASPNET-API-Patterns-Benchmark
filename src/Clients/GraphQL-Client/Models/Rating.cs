namespace GraphQL_Client.Models;

public class Rating
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string StudentName { get; set; } = default!;
    
    public string Review { get; set; } = default!;

    public int StarValue { get; set; }
}