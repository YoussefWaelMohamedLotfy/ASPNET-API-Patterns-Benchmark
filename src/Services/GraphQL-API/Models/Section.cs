namespace GraphQL_API.Models;

public class Section
{
    public int Id { get; set; }
    
    public int CourseId { get; set; }
    
    public int SeqNo { get; set; }
    
    public string Title { get; set; } = default!;

    public List<Lecture> Lectures { get; set; } = default!;
}