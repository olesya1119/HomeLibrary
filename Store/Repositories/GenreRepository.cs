using Dapper;
using Store.Models;
using System.Data;

namespace Store.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GenreRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Genre> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection
                .Query<Genre>(
                    "GenreGetAll",
                    commandType: CommandType.StoredProcedure);
        }
    }

    public Genre? GetById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            return connection.QuerySingleOrDefault<Genre>(
                "GenreGetById",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Create(Genre genre)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "GenreInsert",
                new
                {
                    genre.Name
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Update(Genre genre)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "GenreUpdate",
                new
                {
                    genre.Id,
                    genre.Name
                },
                commandType: CommandType.StoredProcedure);
        }
    }

    public void Delete(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Execute(
                "GenreDelete",
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
