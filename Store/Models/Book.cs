namespace Store.Models;

public class Book
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? PublicationYear { get; set; }

    public string ISBN { get; set; }

    public int PagesNumber { get; set; }

    public long PublisherId { get; set; }

    public string PublisherName { get; set; }

    public string Authors { get; set; }

    public string Genres { get; set; }
}