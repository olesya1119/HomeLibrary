using Store.Models;

namespace Store.Repositories;

public interface IAuthorRepository
{
    IEnumerable<Author> GetAll();

    Author? GetById(long id);

    void Create(Author author);

    void Update(Author author);

    void Delete(long id);
}
