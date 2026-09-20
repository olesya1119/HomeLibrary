USE HomeLibrary;
GO

CREATE OR ALTER PROCEDURE AuthorInsert
    @FirstName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @MiddleName NVARCHAR(20),
    @BirthDate DATE,
    @DeathDate DATE,
    @Description NVARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    IF @BirthDate IS NOT NULL 
       AND @DeathDate IS NOT NULL 
       AND @DeathDate < @BirthDate
        THROW 50001, 'DeathDate cannot be earlier than BirthDate.', 1;

    INSERT INTO Author(FirstName, LastName, MiddleName, BirthDate, DeathDate, Description)
    VALUES (@FirstName, @LastName, @MiddleName, @BirthDate, @DeathDate, @Description);
END;
GO


CREATE OR ALTER PROCEDURE AuthorUpdate
    @Id BIGINT,
    @FirstName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @MiddleName NVARCHAR(20),
    @BirthDate DATE,
    @DeathDate DATE,
    @Description NVARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Author WHERE Id = @Id)
        THROW 50002, 'Author not found.', 1;

    IF @BirthDate IS NOT NULL 
       AND @DeathDate IS NOT NULL 
       AND @DeathDate < @BirthDate
        THROW 50003, 'DeathDate cannot be earlier than BirthDate.', 1;

    UPDATE Author
    SET FirstName = @FirstName,
        LastName = @LastName,
        MiddleName = @MiddleName,
        BirthDate = @BirthDate,
        DeathDate = @DeathDate,
        Description = @Description
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE AuthorDelete
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Author WHERE Id = @Id)
        THROW 50004, 'Author not found.', 1;

    DELETE FROM Author
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE AuthorGetById
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, FirstName, LastName, MiddleName, BirthDate, DeathDate, Description
    FROM Author
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE AuthorGetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, FirstName, LastName, MiddleName, BirthDate, DeathDate, Description
    FROM Author
    ORDER BY LastName, FirstName;
END;
GO