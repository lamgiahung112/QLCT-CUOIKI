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
-- Create Departments table
CREATE TABLE Departments (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    EmployeeID NVARCHAR(255)
);
GO
-- Create Employees table
CREATE TABLE Employees (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Address] NVARCHAR(255) NOT NULL,
    Birth DATETIME NOT NULL,
    EmployeeStatus NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(255) NOT NULL,
    [DepartmentId] NVARCHAR(255) NOT NULL FOREIGN KEY REFERENCES Departments(ID)
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
-- Create Work table
CREATE TABLE Works (
    ID NVARCHAR(255) NOT NULL PRIMARY KEY,
    Assignee NVARCHAR(255) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    Assigner NVARCHAR(255) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
    [Description] NVARCHAR(255) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    StartingTime DateTime NOT NULL,
    EndingTime DateTime NOT NULL,
    [Status] NVARCHAR(20) NOT NULL,
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NOT NULL,    
);

-- Add Department
Insert into Departments Values ('MKT', 'Marketing');
Insert into Departments Values ('ACC', 'Accounting');
Insert into Departments Values ('HRD', 'Human Resources');

-- Add Employees
Insert into Employees Values (N'staffmkt1', N'staff1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Staff', 'MKT');
Insert into Employees Values (N'staffmkt2', N'staff2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Staff', 'MKT');
Insert into Employees Values (N'staffacc1', N'staff3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Staff', 'ACC');
Insert into Employees Values (N'staffacc2', N'staff4', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Staff', 'ACC');
Insert into Employees Values (N'managermkt', N'manager1', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager', 'MKT');
Insert into Employees Values (N'manageracc', N'manager2', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager', 'ACC');
Insert into Employees Values (N'managerhrd', N'manager3', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Manager', 'HRD');
Insert into Employees Values (N'hr', N'hr', N'UTE', '2023-03-04', 'Active', '$2a$11$n8.PUKAHT1KhQfHVYiY8ZOsdgcOh2YvH9eRbeSyNHSr5U/Or70IQ6', 'Male', 'Hr', 'HRD');