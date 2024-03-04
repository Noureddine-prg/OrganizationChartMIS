CREATE TABLE Positions
(
    PositionID NVARCHAR(50) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000),
    ParentPositionID NVARCHAR(50),
    CurrentEmployee NVARCHAR(255) NOT NULL DEFAULT 'Vacant',
    CONSTRAINT FK_Positions_ParentPosition FOREIGN KEY (ParentPositionID)
        REFERENCES Positions (PositionID) 
);

CREATE TABLE Employees
(
    EmployeeID NVARCHAR(50) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PositionID NVARCHAR(50),
    CONSTRAINT FK_Employees_Positions FOREIGN KEY (PositionID)
        REFERENCES Positions (PositionID)
);
