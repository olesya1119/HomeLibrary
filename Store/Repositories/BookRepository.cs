using Dapper;
using Store.Models;
using System.Data;

namespace Store.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BookRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }


    public IEnumerable<Book> GetAll()
    {
        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<Book>(
            "BookGetAll",
            commandType: CommandType.StoredProcedure);
    }


    public BookDetails? GetById(long id)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var result = connection.QueryMultiple(
            "BookGetById",
            new { Id = id },
            commandType: CommandType.StoredProcedure);


        var book = result.Read<BookDetails>()
            .FirstOrDefault();


        if (book == null)
            return null;


        book.AuthorsList = result.Read<Author>().ToList();
        book.GenresList = result.Read<Genre>().ToList();


        return book;
    }


    public void Create(BookDetails book)
    {
        using var connection = _connectionFactory.CreateConnection();

        connection.Execute(
            "BookInsert",
            new
            {
                book.Title,
                book.Description,
                book.PublicationYear,
                book.PublisherId,
                book.ISBN,
                book.PagesNumber,
                book.ContentsTable,

                AuthorIds = CreateIdTable(
                    book.AuthorsList.Select(x => x.Id)),

                GenreIds = CreateIdTable(
                    book.GenresList.Select(x => x.Id))
            },
            commandType: CommandType.StoredProcedure);
    }


    public void Update(BookDetails book)
    {
        using var connection = _connectionFactory.CreateConnection();

        connection.Execute(
            "BookUpdate",
            new
            {
                book.Id,
                book.Title,
                book.Description,
                book.PublicationYear,
                book.PublisherId,
                book.ISBN,
                book.PagesNumber,
                book.ContentsTable,

                AuthorIds = CreateIdTable(
                    book.AuthorsList.Select(x => x.Id)),

                GenreIds = CreateIdTable(
                    book.GenresList.Select(x => x.Id))
            },
            commandType: CommandType.StoredProcedure);
    }


    public void Delete(long id)
    {
        using var connection = _connectionFactory.CreateConnection();

        connection.Execute(
            "BookDelete",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }


    private static SqlMapper.ICustomQueryParameter CreateIdTable(IEnumerable<long> ids)
    {
        var table = new DataTable();

        table.Columns.Add("Id", typeof(long));

        foreach (var id in ids)
        {
            table.Rows.Add(id);
        }

        return table.AsTableValuedParameter("IdList");
    }
}
