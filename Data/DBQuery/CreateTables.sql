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
