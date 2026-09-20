USE HomeLibrary;
GO

CREATE OR ALTER PROCEDURE PublisherInsert
    @Name NVARCHAR(100),
    @Address NVARCHAR(200),
    @Phone NVARCHAR(15),
    @Email NVARCHAR(30),
    @Website NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Email NOT LIKE '%_@_%._%'
        THROW 50001, 'Invalid email format.', 1;

    IF @Phone LIKE '%[^0-9+]%'
        THROW 50002, 'Invalid phone format.', 1;

    INSERT INTO Publisher(Name, Address, Phone, Email, Website)
    VALUES (@Name, @Address, @Phone, @Email, @Website);
END;
GO


CREATE OR ALTER PROCEDURE PublisherUpdate
    @Id BIGINT,
    @Name NVARCHAR(100),
    @Address NVARCHAR(200),
    @Phone NVARCHAR(15),
    @Email NVARCHAR(30),
    @Website NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Publisher WHERE Id = @Id)
        THROW 50003, 'Publisher not found.', 1;

    IF @Email NOT LIKE '%_@_%._%'
        THROW 50004, 'Invalid email format.', 1;

    IF @Phone LIKE '%[^0-9+]%'
        THROW 50005, 'Invalid phone format.', 1;

    UPDATE Publisher
    SET Name = @Name,
        Address = @Address,
        Phone = @Phone,
        Email = @Email,
        Website = @Website
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE PublisherDelete
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Publisher WHERE Id = @Id)
        THROW 50002, 'Publisher not found.', 1;

    DELETE FROM Publisher
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE PublisherGetById
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name, Address, Phone, Email, Website
    FROM Publisher
    WHERE Id = @Id;
END;
GO


CREATE OR ALTER PROCEDURE PublisherGetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name, Address, Phone, Email, Website
    FROM Publisher
    ORDER BY Name;
END;
GO