-- If database "companyDB" not exist, create a new one
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'companyDB')
BEGIN
	CREATE DATABASE companyDB
END
-- If the database 'companyDB' already exists, drop it and create a new one
-- This code to avoid 'Cannot drop database because it is currently in use' error
-- You can disconnect everyone and roll back their transactions with:
GO
USE MASTER;
GO
ALTER DATABASE companyDB
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
-- After that, you can safely drop the database :)
GO
DROP DATABASE IF EXISTS companyDB;
CREATE DATABASE companyDB
-- Working on managementDB
USE companyDB;
-- Create Employees table
CREATE TABLE Employees (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Address] NVARCHAR(255) NOT NULL,
    Birth DATETIME NOT NULL,
    EmployeeStatus NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(255) NOT NULL
);
-- Create WorkSessions table
CREATE TABLE WorkSessions (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    EmployeeID NVARCHAR(255) FOREIGN KEY REFERENCES Employees(ID),
    StartingTime DATETIME NOT NULL,
    EndingTime DATETIME
);
-- Add records to Employees table
INSERT INTO Employees (ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender)
VALUES ('01', N'Phạm Chiến Thắng', N'Tp. Hồ Chí Minh', '1999-2-3', 'Active', 'victorydak', 'Male');
INSERT INTO Employees (ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender)
VALUES ('02', N'Võ Trọng Tín', N'Tp. Hồ Chí Minh', '1998-8-8', 'Active', 'tincuibap', 'Male');
INSERT INTO Employees (ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender)
VALUES ('03', N'Lâm Gia Hưng', N'Tp. Hồ Chí Minh', '1996-1-1', 'Active', 'trikohung', 'Male');
-- Add records to WorkSessions table
INSERT INTO WorkSessions(ID, EmployeeID, StartingTime, EndingTime)
VALUES ('013243', '03', '2023-12-23T08:25:10', '2023-12-23T17:05:10');
INSERT INTO WorkSessions(ID, EmployeeID, StartingTime, EndingTime)
VALUES ('032323', '01', '2023-12-22T08:15:00', '2023-12-22T18:05:10');
INSERT INTO WorkSessions(ID, EmployeeID, StartingTime, EndingTime)
VALUES ('313213', '02', '2023-12-21T08:08:00', '2023-12-21T17:00:10');
-- Check if success
SELECT * FROM Employees
GO
SELECT * FROM WorkSessions