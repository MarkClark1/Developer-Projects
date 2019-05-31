USE master
GO
 
 IF EXISTS(
   SELECT *
   FROM sys.server_principals
   WHERE [Name] = 'DvdLibraryApp')
BEGIN
   DROP LOGIN DvdLibraryApp
END
GO

CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

 USE DvdLibrary
 GO

 IF EXISTS(
   SELECT *
   FROM sys.database_principals
   WHERE [Name] = 'DvdLibraryApp')
BEGIN
   DROP User DvdLibraryApp
END
GO

CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp

GRANT SELECT ON Dvd TO DvdLibraryApp
GRANT INSERT ON Dvd TO DvdLibraryApp
GRANT UPDATE ON Dvd TO DvdLibraryApp
GRANT DELETE ON Dvd TO DvdLibraryApp

GRANT EXECUTE ON DvdSelectAll TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectById TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByTitle TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByYear TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByRating TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByDirector TO DvdLibraryApp
GRANT EXECUTE ON DvdInsert TO DvdLibraryApp
GRANT EXECUTE ON DvdUpdate TO DvdLibraryApp
GRANT EXECUTE ON DvdDelete TO DvdLibraryApp
GO

--The DvdLibrarySecurity.sql file must perform the following actions.
--Create a server login named ‘DvdLibraryApp’ with a password of ‘testing123’.
--Create a database account for ‘DvdLibraryApp’.
--Grant Execute on all used stored procedures to ‘DvdLibraryApp’
--Grant SELECT, INSERT, UPDATE, and DELETE on all used tables to ‘DvdLibraryApp’