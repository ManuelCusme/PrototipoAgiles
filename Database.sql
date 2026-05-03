USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SeguridadUtaDB')
BEGIN
    CREATE DATABASE SeguridadUtaDB;
END
GO

USE SeguridadUtaDB;
GO

-- Users Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE Users (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Nombre1 NVARCHAR(50) NOT NULL,
        Nombre2 NVARCHAR(50),
        Apellido1 NVARCHAR(50) NOT NULL,
        Apellido2 NVARCHAR(50),
        Email NVARCHAR(100) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(MAX) NOT NULL,
        BirthDate DATETIME NOT NULL,
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END

-- Geofences Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Geofences]') AND type in (N'U'))
BEGIN
    CREATE TABLE Geofences (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Name NVARCHAR(100) NOT NULL,
        Latitude FLOAT NOT NULL,
        Longitude FLOAT NOT NULL,
        Radius FLOAT NOT NULL -- In meters
    );
END

-- Incidents Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Incidents]') AND type in (N'U'))
BEGIN
    CREATE TABLE Incidents (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        UserId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(Id),
        Latitude FLOAT NOT NULL,
        Longitude FLOAT NOT NULL,
        GeofenceName NVARCHAR(100),
        Timestamp DATETIME DEFAULT GETDATE()
    );
END

-- Seed Geofences
IF NOT EXISTS (SELECT * FROM Geofences WHERE Name = 'Campus Huachi')
BEGIN
    INSERT INTO Geofences (Id, Name, Latitude, Longitude, Radius)
    VALUES (NEWID(), 'Campus Huachi', -1.2692, -78.6242, 500);
END

IF NOT EXISTS (SELECT * FROM Geofences WHERE Name = 'Campus Ingahurco')
BEGIN
    INSERT INTO Geofences (Id, Name, Latitude, Longitude, Radius)
    VALUES (NEWID(), 'Campus Ingahurco', -1.2422, -78.6251, 300);
END

IF NOT EXISTS (SELECT * FROM Geofences WHERE Name = 'Facultad Sistemas')
BEGIN
    INSERT INTO Geofences (Id, Name, Latitude, Longitude, Radius)
    VALUES (NEWID(), 'Facultad Sistemas', -1.2655, -78.6210, 100);
END
GO
