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
-- Create Projects table
CREATE TABLE Projects (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    ManagerID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    CreatedAt DateTime NOT NULL
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
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    StageID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Stages(ID),
    TechLeadID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    [Name] NVARCHAR(255) NOT NULL
);


GO
-- Create TeamMembers table
CREATE TABLE TeamMembers (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    TeamID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Teams(ID),
    EmployeeID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
);

GO
-- Create Tasks table
CREATE TABLE Tasks (
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    Assignee NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    Assigner NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    TeamID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Teams(ID),
    [Description] NVARCHAR(255) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    StartingTime DateTime NOT NULL,
    EndingTime DateTime NOT NULL,
    [Status] NVARCHAR(255) NOT NULL,
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NOT NULL
);

GO

CREATE TABLE Salary(
    ID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID) PRIMARY KEY,
    basicPay int NOT NULL,
    kpiCost int NOT NULL,
    leaveCost int NOT NULL
);

GO

CREATE TABLE WorkLeaves(
    ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    EmployeeID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    FromDate DateTime NOT NULL,
    ToDate DateTime NOT NULL,
    ReasonOfLeave NVARCHAR(255) NOT NULL
);

-- Add Employees
Insert into Employees Values (N'dev1', N'staff1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'dev2', N'staff2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'dev3', N'staff2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Dev');
Insert into Employees Values (N'des', N'staff3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Designer')
Insert into Employees Values (N'tester', N'staff4', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Tester')
Insert into Employees Values (N'tl1', N'techlead1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'TechLead')
Insert into Employees Values (N'tl2', N'techlead2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'TechLead')
Insert into Employees Values (N'mng1', N'manager3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager');
Insert into Employees Values (N'mng2', N'manager3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager');
Insert into Employees Values (N'hr', N'hr', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Hr');

-- Add Salary
Insert into Salary Values (N'dev1', 300000000, 300000, 50000);
Insert into Salary Values (N'dev2', 50000000, 50000, 50000);
Insert into Salary Values (N'dev3',124283818, 424222, 50000);
Insert into Salary Values (N'des', 32131288, 2131223, 50000)
Insert into Salary Values (N'tester',3213281, 23813821, 50000)
Insert into Salary Values (N'tl1', 31223172, 231883218, 50000)
Insert into Salary Values (N'tl2', 123812828, 2393219, 50000)
Insert into Salary Values (N'mng1',231321321, 3123312, 50000);
Insert into Salary Values (N'mng2', 12312332, 3123222, 50000);
Insert into Salary Values (N'hr', 1231231231, 5532523, 50000);

use companydb;

select * from teams;

