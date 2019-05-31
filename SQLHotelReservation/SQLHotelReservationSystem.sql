IF(db_id(N'HotelReservationSystem') IS NULL)
    BEGIN
        CREATE DATABASE [HotelReservationSystem]
    END;

USE [HotelReservationSystem]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop constraints if table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Room') ALTER TABLE dbo.[Room] DROP CONSTRAINT IF EXISTS fk_Room_RoomType
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Guest') ALTER TABLE dbo.Guest DROP CONSTRAINT IF EXISTS fk_Guest_Guest
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'ContactInfo') ALTER TABLE dbo.ContactInfo DROP CONSTRAINT IF EXISTS fk_ContactInfo_Guest
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Reservation') ALTER TABLE dbo.Reservation DROP CONSTRAINT IF EXISTS fk_Reservation_ContactInfo
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Reservation') ALTER TABLE dbo.Reservation DROP CONSTRAINT IF EXISTS fk_Reservation_Promotion
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'AddOn') ALTER TABLE dbo.AddOn DROP CONSTRAINT IF EXISTS fk_AddOn_Reservation
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Invoice') ALTER TABLE dbo.Invoice DROP CONSTRAINT IF EXISTS fk_Invoice_Reservation
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'ReservationRoom') ALTER TABLE dbo.ReservationRoom DROP CONSTRAINT IF EXISTS fk_ReservationRoom_Room
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'ReservationRoom') ALTER TABLE dbo.ReservationRoom DROP CONSTRAINT IF EXISTS fk_ReservationRoom_Reservation
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'RoomAmenity') ALTER TABLE dbo.RoomAmenity DROP CONSTRAINT IF EXISTS fk_RoomAmenity_Room
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'RoomAmenity') ALTER TABLE dbo.RoomAmenity DROP CONSTRAINT IF EXISTS fk_RoomAmenity_Amenity

--drop tables if they exist
DROP TABLE IF EXISTS dbo.RoomType
DROP TABLE IF EXISTS dbo.[Room]
DROP TABLE IF EXISTS dbo.Guest
DROP TABLE IF EXISTS dbo.ContactInfo
DROP TABLE IF EXISTS dbo.Promotion
DROP TABLE IF EXISTS dbo.Reservation
DROP TABLE IF EXISTS dbo.Invoice
DROP TABLE IF EXISTS dbo.ReservationRoom
DROP TABLE IF EXISTS dbo.AddOn
DROP TABLE IF EXISTS dbo.Amenity
DROP TABLE IF EXISTS dbo.RoomAmenity

--create tables and constraints
CREATE TABLE dbo.RoomType (
	RoomTypeID INT PRIMARY KEY IDENTITY(1,1),
	RoomName NVARCHAR (100) NOT NULL,
	Occupancy INT NOT NULL,
	RoomPrice DECIMAL NOT NULL)
GO

CREATE TABLE dbo.[Room](
	RoomNumber INT PRIMARY KEY IDENTITY(1,1),
	RoomTypeID INT NOT NULL,
	[Floor] INT NOT NULL)	
GO

ALTER TABLE dbo.Room  WITH CHECK ADD CONSTRAINT fk_Room_RoomType FOREIGN KEY(RoomTypeID)
REFERENCES dbo.RoomType (RoomTypeID)
GO
ALTER TABLE dbo.Room CHECK CONSTRAINT fk_Room_RoomType
GO

CREATE TABLE dbo.Guest (
	GuestID INT PRIMARY KEY IDENTITY(1,1),
	GuestOf INT NULL,
	FirstName NVARCHAR (50) NOT NULL,
	LastName NVARCHAR (50) NOT NULL)
GO

ALTER TABLE dbo.Guest  WITH CHECK ADD  CONSTRAINT fk_Guest_Guest FOREIGN KEY(GuestID)
REFERENCES dbo.Guest (GuestID)
GO
ALTER TABLE dbo.Guest CHECK CONSTRAINT fk_Guest_Guest
GO

CREATE TABLE dbo.Promotion (
	PromotionID INT PRIMARY KEY IDENTITY(1,1),
	Discount DECIMAL NOT NULL,
	FlatDiscount DECIMAL NOT NULL,
	StartDate DATE NOT NULL,
	ExpirationDate DATE NOT NULL)
GO

CREATE TABLE dbo.ContactInfo (
	ContactInfoID INT PRIMARY KEY IDENTITY(1,1),
	GuestID INT NOT NULL,
	Email NVARCHAR (100) NOT NULL,
	PhoneNumber NVARCHAR (15) NOT NULL)
GO

ALTER TABLE dbo.ContactInfo  WITH CHECK ADD  CONSTRAINT fk_ContactInfo_Guest FOREIGN KEY(GuestID)
REFERENCES dbo.Guest (GuestID)
GO
ALTER TABLE dbo.ContactInfo CHECK CONSTRAINT fk_ContactInfo_Guest
GO

CREATE TABLE dbo.Reservation (
	ReservationID INT PRIMARY KEY IDENTITY(1,1),
	ContactInfoID INT NOT NULL,
	PromotionID INT NULL,
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL
	)
GO

ALTER TABLE dbo.Reservation  WITH CHECK ADD  CONSTRAINT fk_Reservation_ContactInfo FOREIGN KEY(ContactInfoID)
REFERENCES dbo.ContactInfo (ContactInfoID)
GO
ALTER TABLE dbo.Reservation CHECK CONSTRAINT fk_Reservation_ContactInfo
GO

ALTER TABLE dbo.Reservation  WITH CHECK ADD  CONSTRAINT fk_Reservation_Promotion FOREIGN KEY(PromotionID)
REFERENCES dbo.Promotion (PromotionID)
GO
ALTER TABLE dbo.Reservation CHECK CONSTRAINT fk_Reservation_Promotion
GO

CREATE TABLE dbo.AddOn (
	AddOnID INT PRIMARY KEY IDENTITY(1,1),
	ReservationID INT NOT NULL,
	AddOnName NVARCHAR (100) NOT NULL,
	AddOnPrice DECIMAL NOT NULL)
GO

ALTER TABLE dbo.AddOn  WITH CHECK ADD  CONSTRAINT fk_AddOn_Reservation FOREIGN KEY(ReservationID)
REFERENCES dbo.Reservation (ReservationID)
GO
ALTER TABLE dbo.AddOn CHECK CONSTRAINT fk_AddOn_Reservation
GO 

CREATE TABLE dbo.Invoice (
	InvoiceID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ReservationID INT NOT NULL,
	InvoicePrice DECIMAL NOT NULL,
	InvoiceStatus BIT NOT NULL)
GO

ALTER TABLE dbo.Invoice  WITH CHECK ADD  CONSTRAINT fk_Invoice_Reservation FOREIGN KEY(ReservationID)
REFERENCES dbo.Reservation (ReservationID)
GO
ALTER TABLE dbo.Invoice CHECK CONSTRAINT fk_Invoice_Reservation
GO

CREATE TABLE ReservationRoom (
	RoomNumber INT NOT NULL,
	ReservationID INT NOT NULL,
	PRIMARY KEY (RoomNumber ASC, ReservationID ASC) ON [PRIMARY])
GO

ALTER TABLE dbo.ReservationRoom  WITH CHECK ADD  CONSTRAINT fk_ReservationRoom_Room FOREIGN KEY(RoomNumber)
REFERENCES dbo.Room (RoomNumber)
GO
ALTER TABLE dbo.ReservationRoom CHECK CONSTRAINT fk_ReservationRoom_Room
GO

ALTER TABLE dbo.ReservationRoom  WITH CHECK ADD  CONSTRAINT fk_ReservationRoom_Reservation FOREIGN KEY(ReservationID)
REFERENCES dbo.Reservation (ReservationID)
GO
ALTER TABLE dbo.ReservationRoom CHECK CONSTRAINT fk_ReservationRoom_Reservation
GO

CREATE TABLE Amenity (
	AmenityID INT PRIMARY KEY IDENTITY(1,1),
	AmenityName NVARCHAR (100) NOT NULL,
	AmenityPrice DECIMAL NOT NULL)
GO

CREATE TABLE RoomAmenity (
	RoomNumber INT NOT NULL,
	AmenityID INT NOT NULL,
	PRIMARY KEY CLUSTERED (RoomNumber ASC, AmenityID ASC) ON [PRIMARY])
GO

ALTER TABLE dbo.RoomAmenity  WITH CHECK ADD  CONSTRAINT fk_RoomAmenity_Room FOREIGN KEY(RoomNumber)
REFERENCES dbo.Room (RoomNumber)
GO
ALTER TABLE dbo.RoomAmenity CHECK CONSTRAINT fk_RoomAmenity_Room
GO
ALTER TABLE dbo.RoomAmenity  WITH CHECK ADD  CONSTRAINT fk_RoomAmenity_Amenity FOREIGN KEY(AmenityID)
REFERENCES dbo.Amenity (AmenityID)
GO
ALTER TABLE dbo.RoomAmenity CHECK CONSTRAINT fk_RoomAmenity_Amenity
GO

--Insert into RoomType Table
INSERT INTO [dbo].[RoomType]([RoomName],[Occupancy],[RoomPrice])VALUES('1 King Bed Studio w/ Hideaway Bed', 3, 200.00)
INSERT INTO [dbo].[RoomType]([RoomName],[Occupancy],[RoomPrice])VALUES('2 Queen Standard', 4, 160.00)
INSERT INTO [dbo].[RoomType]([RoomName],[Occupancy],[RoomPrice])VALUES('2 Queen Studio w/ Pull Out Sofa', 5, 180.00)
INSERT INTO [dbo].[RoomType]([RoomName],[Occupancy],[RoomPrice])VALUES('1 King Whirlpool Suite w/ Hideaway Bed', 3, 240.00)
INSERT INTO [dbo].[RoomType]([RoomName],[Occupancy],[RoomPrice])VALUES('1 Queen Standard', 3, 140.00)
GO


-- Insert Into Room Table
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(1, 3)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(2, 1)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(3, 3)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(4, 4)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(5, 2)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(5, 1)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(3, 2)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(1, 4)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(4, 3)
INSERT INTO [dbo].[Room]([RoomTypeID],[Floor])VALUES(2, 2)
GO

--Insert into Guest Table
INSERT INTO [dbo].[Guest]([GuestOf],[FirstName],[LastName])VALUES(null,'Anthony','Dahl')
INSERT INTO [dbo].[Guest]([GuestOf],[FirstName],[LastName])VALUES(null,'Bruce','Wayne')
INSERT INTO [dbo].[Guest]([GuestOf],[FirstName],[LastName])VALUES(null,'Luke','Skywalker')
INSERT INTO [dbo].[Guest]([GuestOf],[FirstName],[LastName])VALUES(null,'Ryan','Reynolds')
INSERT INTO [dbo].[Guest]([GuestOf],[FirstName],[LastName])VALUES(null,'Fox','McCloud')
GO

--Insert into Contactinfo Table
INSERT INTO [dbo].[ContactInfo]([GuestID],[Email],[PhoneNumber])VALUES(1,'adahl@gmail.com','111-222-1111')
INSERT INTO [dbo].[ContactInfo]([GuestID],[Email],[PhoneNumber])VALUES(2,'darknight@gmail.com','111-222-2222')
INSERT INTO [dbo].[ContactInfo]([GuestID],[Email],[PhoneNumber])VALUES(3,'thelightside@gmail.com','111-222-3333')
INSERT INTO [dbo].[ContactInfo]([GuestID],[Email],[PhoneNumber])VALUES(3,'deadpool@gmail.com','111-222-4444')
INSERT INTO [dbo].[ContactInfo]([GuestID],[Email],[PhoneNumber])VALUES(3,'destroyandros@gmail.com','111-222-5555')
GO

--Insert into Promotion Table
INSERT INTO [dbo].[Promotion]([Discount],[FlatDiscount],[StartDate],[ExpirationDate])VALUES(10.00,10.00,'2020/08/11','2020/12/31')
INSERT INTO [dbo].[Promotion]([Discount],[FlatDiscount],[StartDate],[ExpirationDate])VALUES(20.00,20.00,'2020/01/01','2021/12/31')
INSERT INTO [dbo].[Promotion]([Discount],[FlatDiscount],[StartDate],[ExpirationDate])VALUES(50.00,30.00,'2019/02/01','2019/12/31')
INSERT INTO [dbo].[Promotion]([Discount],[FlatDiscount],[StartDate],[ExpirationDate])VALUES(50.00,50.00,'2019/01/01','2019/04/30')
INSERT INTO [dbo].[Promotion]([Discount],[FlatDiscount],[StartDate],[ExpirationDate])VALUES(10.00,100.00,'2019/04/01','2019/01/02')
GO

--Insert into Reservation Table
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(1, '2020/11/11', '2020/11/12', 1)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(2, '2020/05/11', '2020/05/13', NULL)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(2, '2020/05/11', '2020/05/13', 2)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(3, '2020/01/01', '2020/01/02', 3)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(3, '2020/02/11', '2020/02/12', 4)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(1, '2020/03/01', '2020/03/02', 4)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(4, '2020/04/08', '2020/04/09', 2)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(5, '2020/05/10', '2020/05/11', 5)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(3, '2020/07/08', '2020/07/11', NULL)
INSERT INTO [dbo].[Reservation]([ContactInfoID],[CheckIn],[CheckOut],[PromotionID])VALUES(4, '2019/02/25', '2019/02/27', 2)
GO

--Insert into AddOn Table
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(1, 'Champagne', 40.00)
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(1, 'Wine', 40.00)
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(2, 'Room Service', 100.00)
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(1, 'Movie Rental', 20.00)
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(1, 'Message', 200.00)
INSERT INTO [dbo].[AddOn]([ReservationID],[AddOnName],[AddOnPrice])VALUES(1, 'Gaming Rental', 60.00)
GO

--Insert into Invoice Table
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(1, 200.00, 'True')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(2, 160.00, 'False')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(3, 180.00, 'True')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(4, 240.00, 'True')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(5, 140.00, 'False')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(6, 140.00, 'True')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(7, 180.00, 'False')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(8, 200.00, 'False')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(9, 240.00, 'True')
INSERT INTO [dbo].[Invoice]([ReservationID],[InvoicePrice],[InvoiceStatus])VALUES(10, 160.00, 'True')
GO

--Insert into ReservationRoom Table
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(1, 1)
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(2, 2)
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(3, 3)
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(4, 5)
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(5, 1)
INSERT INTO [dbo].[ReservationRoom]([RoomNumber],[ReservationID])VALUES(6, 4)
GO

--Insert into Amenity Table
INSERT INTO [dbo].[Amenity]([AmenityName],[AmenityPrice])VALUES('Hideaway Bed', 20.00)
INSERT INTO [dbo].[Amenity]([AmenityName],[AmenityPrice])VALUES('Whirlpool', 100.00)
INSERT INTO [dbo].[Amenity]([AmenityName],[AmenityPrice])VALUES('Delux Entertainment System', 80.00)
INSERT INTO [dbo].[Amenity]([AmenityName],[AmenityPrice])VALUES('Fridge', 40.00)
INSERT INTO [dbo].[Amenity]([AmenityName],[AmenityPrice])VALUES('Kitchenette', 80.00)
GO

--Insert into RoomAmenity Table
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(2, 1)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(2, 2)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(3, 3)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(4, 3)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(4, 4)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(6, 1)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(7, 5)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(8, 3)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(9, 2)
INSERT INTO [dbo].[RoomAmenity]([RoomNumber],[AmenityID])VALUES(8, 4)
GO


--1
SELECT *
FROM Reservation
WHERE Reservation.CheckOut = '2019/02/27'

--2
SELECT *
FROM Reservation
WHERE ContactInfoID = 2

--3
SELECT *
FROM Room
INNER JOIN RoomAmenity ON RoomAmenity.RoomNumber = Room.RoomNumber
WHERE RoomAmenity.AmenityID != 2


--SELECT *
--FROM dbo.Room
--WHERE Floor = 1

--Create Views
--CREATE VIEW dbo.GuestContactInfo  
--AS  
--SELECT ge.GuestID, ge.FirstName, ge.LastName, ci.ContactInfoID, ci.Email, ci.PhoneNumber  
--FROM dbo.Guest ge
--JOIN dbo.ContactInfo ci ON ci.GuestID = ge.GuestID
--GO  

	--[ClientId] [uniqueidentifier] NOT NULL,
	--[FirstName] [varchar](50) NOT NULL,
	--[LastName] [varchar](50) NOT NULL,
	--[BirthDate] [date] NULL,
	--[Address] [varchar](256) NULL,
	--[City] [varchar](100) NULL,
	--[StateAbbr] [char](2) NULL,
	--[PostalCode] [varchar](10) NULL,


