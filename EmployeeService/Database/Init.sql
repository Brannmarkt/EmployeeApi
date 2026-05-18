CREATE DATABASE TestDb;
GO

USE TestDb;
GO

CREATE TABLE [dbo].[Employee] (
    [ID]        INT           NOT NULL PRIMARY KEY,
    [Name]      VARCHAR (100) NOT NULL,
    [ManagerID] INT           NULL,
    [Enable]    BIT DEFAULT 1 NOT NULL
);
GO

INSERT INTO [dbo].[Employee] (ID, Name, ManagerID, Enable) VALUES (1, 'Manager1', NULL, 0);
INSERT INTO [dbo].[Employee] (ID, Name, ManagerID, Enable) VALUES (2, 'Employee1', 1, 1);
INSERT INTO [dbo].[Employee] (ID, Name, ManagerID, Enable) VALUES (3, 'Employee2', 1, 1);
INSERT INTO [dbo].[Employee] (ID, Name, ManagerID, Enable) VALUES (4, 'Manager2', NULL, 1);
INSERT INTO [dbo].[Employee] (ID, Name, ManagerID, Enable) VALUES (5, 'Employee3', 4, 1);
GO