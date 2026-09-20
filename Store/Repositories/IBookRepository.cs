using Store.Models;

namespace Store.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();

    BookDetails? GetById(long id);

    void Create(BookDetails book);

    void Update(BookDetails book);

    void Delete(long id);
}
