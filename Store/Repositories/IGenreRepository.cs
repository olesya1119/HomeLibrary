using Store.Models;

namespace Store.Repositories;

public interface IGenreRepository
{
    IEnumerable<Genre> GetAll();

    Genre? GetById(long id);

    void Create(Genre genre);

    void Update(Genre genre);

    void Delete(long id);
}
