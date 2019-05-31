IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO



CREATE TABLE Dvd (
DvdId int identity(1,1) not null primary key,
Title nvarchar(100) not null,
ReleaseDate char(4) not null,
Director nvarchar(50) not null,
Rating nvarchar(15) not null,
Notes nvarchar(max) null
)