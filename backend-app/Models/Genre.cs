namespace backend_app.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<Movie> Movies { get; set; }
}