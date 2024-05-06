CREATE TABLE job_status (
    status_name VARCHAR(20) PRIMARY KEY
);

INSERT INTO job_status (status_name) VALUES
('Active'),
('Inactive');

CREATE TABLE department (
    doid VARCHAR(50) PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    reportsTo VARCHAR(50) NULL,
    FOREIGN KEY (reportsTo) REFERENCES department(doid)
);

CREATE TABLE team (
    teamid VARCHAR(50) PRIMARY KEY,
    teamName VARCHAR(255) NOT NULL UNIQUE,
    departmentId VARCHAR(50) NOT NULL,
    FOREIGN KEY (departmentId) REFERENCES department(doid)
);

CREATE TABLE position (
    poid VARCHAR(50) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    level INT NOT NULL,
    reportsTo VARCHAR(50) NULL, 
    departmentId VARCHAR(50) NULL, 
    teamId VARCHAR(50) NULL, 
    FOREIGN KEY (reportsTo) REFERENCES position(poid),
    FOREIGN KEY (departmentId) REFERENCES department(doid),
    FOREIGN KEY (teamId) REFERENCES team(teamid)
);

CREATE TABLE employee (
    emid VARCHAR(50) PRIMARY KEY,
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    reportsTo VARCHAR(50) NULL,
    status VARCHAR(20) NOT NULL,
    positionId VARCHAR(50) NOT NULL,
    FOREIGN KEY (status) REFERENCES job_status(status_name),
    FOREIGN KEY (positionId) REFERENCES position(poid),
    FOREIGN KEY (reportsTo) REFERENCES employee(emid)
);

CREATE TABLE orgnodes (
    nodeId VARCHAR(50) PRIMARY KEY, -- Function for node ID generation
    positionId VARCHAR(50) NOT NULL, -- Position [use this to display position information]
    employeeId VARCHAR(50) NULL,  -- Employee if there is any (create function for adding/removing employee from node)
    departmentId VARCHAR(50) NULL, 
    teamId VARCHAR(50) NULL,   
    reportsToNodeId VARCHAR(50) NULL,  -- Link [this is going to be set to position's reportsTo attribute]
    FOREIGN KEY (PositionId) REFERENCES position(poid),
    FOREIGN KEY (EmployeeId) REFERENCES employee(emid),
    FOREIGN KEY (DepartmentId) REFERENCES department(doid),
    FOREIGN KEY (TeamId) REFERENCES team(teamid),
    FOREIGN KEY (ReportsToNodeId) REFERENCES orgnodes(nodeId)
);




