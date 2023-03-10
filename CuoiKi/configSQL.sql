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

GO
-- Create Employees table
CREATE TABLE Employees (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Address] NVARCHAR(255) NOT NULL,
    Birth DATETIME NOT NULL,
    EmployeeStatus NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(255) NOT NULL,
);
GO
-- Create WorkSessions table
CREATE TABLE WorkSessions (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    EmployeeID NVARCHAR(50) FOREIGN KEY REFERENCES Employees(ID),
    StartingTime DATETIME NOT NULL,
    EndingTime DATETIME
);
GO
-- Create Tasks table
CREATE TABLE Tasks (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    Assignee NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    Assigner NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    [Description] NVARCHAR(255) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    StartingTime DateTime NOT NULL,
    EndingTime DateTime NOT NULL,
    [Status] NVARCHAR(255) NOT NULL,
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NOT NULL
);

GO
-- Create Projects table
CREATE TABLE Projects (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    ManagerId NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID)
);

GO
-- Create Stages table
CREATE TABLE Stages (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    ProjectID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Projects(ID),
    [Description] NVARCHAR(255) NOT NULL,
);

GO
-- Create Teams table
CREATE TABLE Teams (
    TeamID NVARCHAR(50) NOT NULL,
    StageID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Stages(ID),
    [Description] NVARCHAR(255) NOT NULL,
    PRIMARY KEY(TeamID, StageID)
);

GO
-- Create TeamMembers table
CREATE TABLE TeamMembers (
    TeamID NVARCHAR(50) NOT NULL,
    EmployeeID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    PRIMARY KEY (TeamID, EmployeeID)
);

-- Add Employees
Insert into Employees Values (N'dev1', N'staff1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'dev2', N'staff2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'dev3', N'staff2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'des', N'staff3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Designer');
Insert into Employees Values (N'tester', N'staff4', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Tester');
Insert into Employees Values (N'tl1', N'manager1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'TechLead');
Insert into Employees Values (N'tl2', N'manager2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'TechLead');
Insert into Employees Values (N'mng1', N'manager3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager');
Insert into Employees Values (N'mng2', N'manager3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager');
Insert into Employees Values (N'hr', N'hr', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Hr');
