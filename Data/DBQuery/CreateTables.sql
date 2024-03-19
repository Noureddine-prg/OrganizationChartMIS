-- Enumeration tables for job status and hierarchy level
CREATE TABLE job_status (
    status_name VARCHAR(20) PRIMARY KEY
);

INSERT INTO job_status (status_name) VALUES
('Active'),
('Inactive');

CREATE TABLE hierarchy_level (
    level_name VARCHAR(20) PRIMARY KEY
);

INSERT INTO hierarchy_level (level_name) VALUES
('CEO'),
('DepartmentLead'),
('Manager'),
('AssistantManager'),
('Supervisor'),
('Employee');

-- Position Table
CREATE TABLE position (
    poid VARCHAR(50) PRIMARY KEY,
    name VARCHAR(255) UNIQUE,
    department VARCHAR(255),
    hierarchyLevel VARCHAR(20),
    FOREIGN KEY (hierarchyLevel) REFERENCES hierarchy_level(level_name)
);

-- Employee Table
CREATE TABLE employee (
    emid VARCHAR(50) PRIMARY KEY,
    email VARCHAR(255) UNIQUE,
    name VARCHAR(255),
    parentId VARCHAR(50),
    status VARCHAR(20),
    positionId VARCHAR(50),
    FOREIGN KEY (status) REFERENCES job_status(status_name),
    FOREIGN KEY (positionId) REFERENCES position(poid),
    FOREIGN KEY (parentId) REFERENCES employee(emid) 
);