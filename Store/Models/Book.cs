namespace Store.Models;

public class Book
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? PublicationYear { get; set; }

    public long? PublisherId { get; set; }

    public string PublisherName { get; set; }

    public string ISBN { get; set; }

    public int PagesNumber { get; set; }

    // TODO: Здесь впоследствии будет XML из поля ContentsTable.
    public string ContentsTable { get; set; }

    public List<long> AuthorIds { get; set; } = new List<long>();

    public List<long> GenreIds { get; set; } = new List<long>();

    public List<Author> Authors { get; set; } = new List<Author>();

    public List<Genre> Genres { get; set; } = new List<Genre>();
}