namespace Store.Models;

public class Author
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? DeathDate { get; set; }

    public string Description { get; set; }

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}