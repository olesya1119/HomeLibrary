USE HomeLibrary;
GO

CREATE OR ALTER PROCEDURE BookInsert
    @Title NVARCHAR(100),
    @Description NVARCHAR(300),
    @PublicationYear INT,
    @PublisherId BIGINT,
    @ISBN NVARCHAR(50),
    @PagesNumber INT,
    @ContentsTable XML,
    @AuthorIds IdList READONLY,
    @GenreIds IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS(SELECT 1 FROM Publisher WHERE Id = @PublisherId)
            THROW 50001, 'Publisher not found.', 1;


        INSERT INTO Book(Title, Description, PublicationYear, PublisherId, ISBN, PagesNumber, ContentsTable)
        VALUES(@Title, @Description, @PublicationYear, @PublisherId, @ISBN, @PagesNumber, @ContentsTable);


        DECLARE @BookId BIGINT = SCOPE_IDENTITY();


        INSERT INTO BookAuthor(BookId, AuthorId)
        SELECT @BookId, Id
        FROM @AuthorIds;


        INSERT INTO BookGenre(BookId, GenreId)
        SELECT @BookId, Id
        FROM @GenreIds;


        COMMIT TRANSACTION;

        SELECT @BookId AS Id;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE BookUpdate
    @Id BIGINT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(300),
    @PublicationYear INT,
    @PublisherId BIGINT,
    @ISBN NVARCHAR(50),
    @PagesNumber INT,
    @ContentsTable XML,
    @AuthorIds IdList READONLY,
    @GenreIds IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS(SELECT 1 FROM Book WHERE Id = @Id)
            THROW 50002, 'Book not found.', 1;


        IF NOT EXISTS(SELECT 1 FROM Publisher WHERE Id = @PublisherId)
            THROW 50003, 'Publisher not found.', 1;


        UPDATE Book
        SET Title = @Title,
            Description = @Description,
            PublicationYear = @PublicationYear,
            PublisherId = @PublisherId,
            ISBN = @ISBN,
            PagesNumber = @PagesNumber,
            ContentsTable = @ContentsTable
        WHERE Id = @Id;


        DELETE FROM BookAuthor
        WHERE BookId = @Id;


        INSERT INTO BookAuthor(BookId, AuthorId)
        SELECT @Id, Id
        FROM @AuthorIds;


        DELETE FROM BookGenre
        WHERE BookId = @Id;


        INSERT INTO BookGenre(BookId, GenreId)
        SELECT @Id, Id
        FROM @GenreIds;


        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE BookDelete
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;


        IF NOT EXISTS(SELECT 1 FROM Book WHERE Id = @Id)
            THROW 50004, 'Book not found.', 1;


        DELETE FROM BookAuthor
        WHERE BookId = @Id;


        DELETE FROM BookGenre
        WHERE BookId = @Id;


        DELETE FROM Book
        WHERE Id = @Id;


        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE BookGetAll
AS
BEGIN
    SET NOCOUNT ON;

    WITH Authors AS
    (
        SELECT
            ba.BookId,
            STRING_AGG(
                CONCAT(
                    LEFT(a.FirstName, 1), '. ',
                    LEFT(a.MiddleName, 1), '. ',
                    a.LastName
                ),
                ', '
            ) AS Authors
        FROM BookAuthor ba
        JOIN Author a ON a.Id = ba.AuthorId
        GROUP BY ba.BookId
    ),
    Genres AS
    (
        SELECT bg.BookId, STRING_AGG(g.Name, ', ') AS Genres
        FROM BookGenre bg
        JOIN Genre g ON g.Id = bg.GenreId
        GROUP BY bg.BookId
    )

    SELECT b.Id, b.Title, b.Description, b.PublicationYear, b.ISBN, b.PagesNumber, p.Name AS PublisherName, a.Authors, g.Genres
    FROM Book b
    LEFT JOIN Publisher p ON p.Id = b.PublisherId
    LEFT JOIN Authors a ON a.BookId = b.Id
    LEFT JOIN Genres g ON g.BookId = b.Id
    ORDER BY b.Title;
END;
GO


CREATE OR ALTER PROCEDURE BookGetById
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    WITH Authors AS
    (
        SELECT
            ba.BookId,
            STRING_AGG(
                CONCAT(
                    LEFT(a.FirstName, 1), '. ',
                    LEFT(a.MiddleName, 1), '. ',
                    a.LastName
                ),
                ', '
            ) AS Authors
        FROM BookAuthor ba
        JOIN Author a ON a.Id = ba.AuthorId
        WHERE ba.BookId = @Id
        GROUP BY ba.BookId
    ),
    Genres AS
    (
        SELECT
            bg.BookId,
            STRING_AGG(g.Name, ', ') AS Genres
        FROM BookGenre bg
        JOIN Genre g ON g.Id = bg.GenreId
        WHERE bg.BookId = @Id
        GROUP BY bg.BookId
    )

    SELECT b.Id, b.Title, b.Description, b.PublicationYear, b.ISBN, b.PagesNumber, b.ContentsTable, p.Id AS PublisherId, p.Name AS PublisherName, a.Authors, g.Genres
    FROM Book b
    LEFT JOIN Publisher p ON p.Id = b.PublisherId
    LEFT JOIN Authors a ON a.BookId = b.Id
    LEFT JOIN Genres g ON g.BookId = b.Id
    WHERE b.Id = @Id;


    SELECT a.Id, a.FirstName, a.LastName, a.MiddleName, a.BirthDate, a.DeathDate
    FROM Author a
    JOIN BookAuthor ba ON ba.AuthorId = a.Id
    WHERE ba.BookId = @Id
    ORDER BY a.LastName, a.FirstName;


    SELECT
        g.Id,
        g.Name
    FROM Genre g
    JOIN BookGenre bg ON bg.GenreId = g.Id
    WHERE bg.BookId = @Id
    ORDER BY g.Name;

END;
GO