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
    Gender NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(255) NOT NULL
);
GO
-- Create WorkSessions table
CREATE TABLE WorkSessions (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    EmployeeID NVARCHAR(255) FOREIGN KEY REFERENCES Employees(ID),
    StartingTime DATETIME NOT NULL,
    EndingTime DATETIME
);
GO
-- SELECT * FROM Employees
-- DROP TABLE WorkSessions
-- SELECT * FROM WorkSessions WHERE EndingTime is NULL 
-- SELECT TOP 1 * FROM WorkSessions ORDER BY StartingTime DESC
-- SELECT * FROM WorkSessions WHERE WorkSessions.StartingTime = (SELECT MAX(StartingTime) FROM WorkSessions)
/*
WITH EmployeeWorkSessions AS (
    SELECT * FROM WorkSessions WHERE WorkSessions.EmployeeID = N'staff'
) SELECT TOP 1 * FROM EmployeeWorkSessions ORDER BY EmployeeWorkSessions.StartingTime DESC
*/
-- Add admin
Insert into Employees Values (N'staff', N'staff', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Staff');
Insert into Employees Values (N'manager', N'manager', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager');
Insert into Employees Values (N'hr', N'hr', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Hr');