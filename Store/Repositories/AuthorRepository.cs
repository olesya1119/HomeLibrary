using Dapper;
using Store.Models;
using System.Data;

namespace Store.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AuthorRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Author> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection
                .Query<Author>(
                    "AuthorGetAll",
                    commandType: CommandType.StoredProcedure)
                .AsList();
        }
    }

    public Author? GetById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection.QuerySingleOrDefault<Author>(
                "AuthorGetById",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Create(Author author)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "AuthorInsert",
                new
                {
                    author.FirstName,
                    author.LastName,
                    author.MiddleName,
                    author.BirthDate,
                    author.DeathDate,
                    author.Description
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Update(Author author)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "AuthorUpdate",
                new
                {
                    author.Id,
                    author.FirstName,
                    author.LastName,
                    author.MiddleName,
                    author.BirthDate,
                    author.DeathDate,
                    author.Description
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Delete(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "AuthorDelete",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}