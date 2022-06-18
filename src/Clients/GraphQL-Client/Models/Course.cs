namespace GraphQL_Client.Models;

public class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    
    public string Level { get; set; } = default!;
    
    public string Instructor { get; set; } = default!;

    public PaymentType PaymentType { get; set; }

    public int Duration { get; set; }


    //public List<Section> Sections { get; set; }

    public List<Rating> Ratings { get; set; } = default!;
}
