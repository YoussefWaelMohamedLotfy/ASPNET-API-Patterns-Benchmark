namespace GraphQL_API.Models;

public class Assignment : Lecture
{
    public string Instructions { get; set; } = default!;
    
    public string Questions { get; set; } = default!;
}
