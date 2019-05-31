USE DvdLibrary
GO

CREATE PROCEDURE DvdSelectAll AS
BEGIN
	SELECT *
	FROM Dvd
END
GO


CREATE PROCEDURE DvdSelectById (@DvdId int)
AS
BEGIN
	SELECT *
	FROM Dvd
	WHERE Dvd.DvdId = @DvdId
END
GO

CREATE PROCEDURE DvdSelectByTitle (@Title nvarchar(100)) AS
BEGIN
	SELECT *
	FROM Dvd
	WHERE Dvd.Title = @Title
END
GO

CREATE PROCEDURE DvdSelectByYear (@Year char(4)) AS
BEGIN
	SELECT *
	FROM Dvd
	WHERE Dvd.ReleaseDate = @Year
END
GO

CREATE PROCEDURE DvdSelectByRating (@Rating nvarchar(15)) AS
BEGIN
	SELECT *
	FROM Dvd
	WHERE Dvd.Rating = @Rating
END
GO

CREATE PROCEDURE DvdSelectByDirector (@Director nvarchar(50)) AS
BEGIN
	SELECT *
	FROM Dvd
	WHERE Dvd.Director = @Director
END
GO

CREATE PROCEDURE DvdInsert (
	@Title nvarchar(100),
	@Year char(4),
	@Director nvarchar(50),
	@Rating nvarchar(15),
	@Notes nvarchar(max) ) 
AS
BEGIN
	INSERT INTO Dvd(Title, ReleaseDate, Director, Rating, Notes)
	VALUES (@Title, @Year, @Director, @Rating, @Notes)

END
GO

CREATE PROCEDURE DvdUpdate (
	@DvdId int,
	@Title nvarchar(100),
	@Year char(4),
	@Director nvarchar(50),
	@Rating nvarchar(15),
	@Notes nvarchar(max) )
AS
BEGIN
	UPDATE Dvd
	SET 
	Title = @Title,
	ReleaseDate = @Year,
	Director = @Director,
	Rating = @Rating,
	Notes = @Notes
	WHERE Dvd.DvdId = @DvdId
END
GO

CREATE PROCEDURE DvdDelete (@DvdId int)
AS
BEGIN
	DELETE FROM Dvd WHERE Dvd.DvdId = @DvdId
END
GO