using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Models;

public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Genre Genre { get; set; }
    public int GenreId { get; set; }
    
    public IEnumerable<Actor> Actors { get; set; }
}