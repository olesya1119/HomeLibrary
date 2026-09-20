using Dapper;
using Store.Models;
using System.Data;

namespace Store.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PublisherRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Publisher> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection.Query<Publisher>("PublisherGetAll", commandType: CommandType.StoredProcedure);
        }
    }

    public Publisher? GetById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection.QuerySingleOrDefault<Publisher>(
                "PublisherGetById",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Create(Publisher publisher)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "Publisher_Insert",
                new
                {
                    publisher.ShortName,
                    publisher.FullName,
                    publisher.Address,
                    publisher.Phone,
                    publisher.Email,
                    publisher.Website
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Update(Publisher publisher)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "Publisher_Update",
                new
                {
                    publisher.Id,
                    publisher.ShortName,
                    publisher.FullName,
                    publisher.Address,
                    publisher.Phone,
                    publisher.Email,
                    publisher.Website
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Delete(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "PublisherDelete",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}