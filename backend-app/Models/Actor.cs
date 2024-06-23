namespace backend_app.Models;

public class Actor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public IEnumerable<Movie> Movies { get; set; }
}