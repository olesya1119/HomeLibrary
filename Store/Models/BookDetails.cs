namespace Store.Models;

public class BookDetails : Book
{
    public string ContentsTable { get; set; }

    public List<Author> AuthorsList { get; set; } = new();

    public List<Genre> GenresList { get; set; } = new();
}
