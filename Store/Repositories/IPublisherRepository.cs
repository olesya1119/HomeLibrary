using Store.Models;

namespace Store.Repositories;

public interface IPublisherRepository
{
    IEnumerable<Publisher> GetAll();

    Publisher? GetById(long id);

    void Create(Publisher publisher);

    void Update(Publisher publisher);

    void Delete(long id);
}
