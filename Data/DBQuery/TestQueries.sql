INSERT INTO position (poid, name, department, hierarchyLevel) VALUES
('P01', 'Chief Executive Officer', 'Executive', 'CEO'),
('P02', 'Lead Software Engineer', 'Engineering', 'DepartmentLead'),
('P03', 'Software Engineering Manager', 'Engineering', 'Manager'),
('P04', 'Senior Software Engineer', 'Engineering', 'Employee'),
('P05', 'Software Engineer', 'Engineering', 'Employee');


INSERT INTO employee (emid, email, name, parentId, status, positionId) VALUES
('E001', 'ceo@example.com', 'Jordan L.', NULL, 'Active', 'P01'),
('E002', 'lead.dev@example.com', 'Casey O.', 'E001', 'Active', 'P02'),
('E003', 'manager.dev@example.com', 'Alex E.', 'E002', 'Active', 'P03'),
('E004', 'senior.dev@example.com', 'Sam W.', 'E003', 'Active', 'P04'),
('E005', 'junior.dev@example.com', 'Jamie Q.', 'E004', 'Active', 'P05');

-- Insert departments
--INSERT INTO department (doid, name) VALUES
--('D1', 'Finance'),
--('D2', 'IT'),
--('D3', 'HR'),
--('D4', 'Operations'),
--('D5', 'Marketing');
ALTER TABLE position
ALTER COLUMN departmentId VARCHAR(50) NULL;

-- Insert top-level executive positions without department association
INSERT INTO position (poid, name, level, reportsTo, departmentId) VALUES
('P1', 'CEO', 1, NULL, NULL),
('P2', 'CFO', 2, 'P1', NULL),
('P3', 'CIO', 2, 'P1', NULL),
('P12', 'COO', 2, 'P1', NULL);

-- Insert departments as positions linked to executives and corresponding to department entries [temp for now but good for organizing]
INSERT INTO position (poid, name, level, reportsTo, departmentId) VALUES
('P4', 'Finance Department', 3, 'P2', 'D1'),
('P5', 'IT Department', 3, 'P3', 'D2'),
('P8', 'HR Department', 3, 'P2', 'D3'),
('P13', 'Operations Department', 3, 'P12', 'D4'),
('P14', 'Marketing Department', 3, 'P12', 'D5');

-- Insert specific roles within departments, making sure they're linked to the department positions
INSERT INTO position (poid, name, level, reportsTo, departmentId, teamId) VALUES
('P6', 'Finance Manager', 4, 'P4', 'D1', NULL),
('P7', 'IT Manager', 4, 'P5', 'D2', NULL),
('P9', 'HR Manager', 4, 'P8', 'D3', NULL),
('P10', 'Senior Accountant', 5, 'P6', 'D1', NULL),
('P11', 'System Administrator', 5, 'P7', 'D2', NULL),
('P15', 'Accounting Specialist', 5, 'P6', 'D1', NULL),
('P16', 'Financial Analyst', 5, 'P6', 'D1', NULL),
('P17', 'Lead Developer', 5, 'P7', 'D2', 'T2'),
('P18', 'QA Engineer', 5, 'P7', 'D2', 'T2'),
('P19', 'HR Coordinator', 5, 'P9', 'D3', NULL),
('P20', 'Operations Manager', 4, 'P13', 'D4', NULL),
('P21', 'Logistics Coordinator', 5, 'P20', 'D4', NULL),
('P22', 'Marketing Manager', 4, 'P14', 'D5', NULL),
('P23', 'Content Strategist', 5, 'P22', 'D5', NULL),
('P24', 'Social Media Specialist', 5, 'P22', 'D5', NULL);


-- Teams within departments
--INSERT INTO team (teamid, teamName, departmentId) VALUES
--('T1', 'Network Security Team A', 'D2'),
--('T2', 'Software Development Team', 'D2');

-- Associate more positions with teams
INSERT INTO position (poid, name, level, reportsTo, departmentId, teamId) VALUES
('P25', 'Application Developer', 6, 'P17', 'D2', 'T3'),
('P26', 'Digital Marketing Analyst', 6, 'P22', 'D5', 'T4');

-- Top-Level Executives
INSERT INTO employee (emid, email, name, reportsTo, status, positionId) VALUES
('E1', 'ceo@company.com', 'Jordan Smith', NULL, 'Active', 'P1'),
('E2', 'cfo@company.com', 'Morgan Lee', 'E1', 'Active', 'P2'),
('E3', 'cio@company.com', 'Taylor Brown', 'E1', 'Active', 'P3'),
('E12', 'coo@company.com', 'Charlie Torres', 'E1', 'Active', 'P12');

-- Department Managers and Specialized Roles
INSERT INTO employee (emid, email, name, reportsTo, status, positionId) VALUES
('E4', 'financemanager@company.com', 'Jamie White', 'E2', 'Active', 'P6'),
('E5', 'itmanager@company.com', 'Sam Rivera', 'E3', 'Active', 'P7'),
('E9', 'hrmanager@company.com', 'Robin Green', 'E2', 'Active', 'P9'),
('E13', 'opsmanager@company.com', 'Morgan Brown', 'E12', 'Active', 'P20'),
('E14', 'markmanager@company.com', 'Taylor Martin', 'E12', 'Active', 'P22');

-- Specialists and Coordinators
INSERT INTO employee (emid, email, name, reportsTo, status, positionId) VALUES
('E6', 'senioraccountant@company.com', 'Casey Davis', 'E4', 'Active', 'P10'),
('E7', 'sysadmin@company.com', 'Alex Johnson', 'E5', 'Active', 'P11'),
('E8', 'accountspec@company.com', 'Dylan Murphy', 'E4', 'Active', 'P15'),
('E10', 'finanalyst@company.com', 'Jordan Bailey', 'E4', 'Active', 'P16'),
('E11', 'hrcoordinator@company.com', 'Jamie Garcia', 'E9', 'Active', 'P19'),
('E15', 'logistics@company.com', 'Charlie Kim', 'E13', 'Active', 'P21'),
('E16', 'contentstrat@company.com', 'Casey Robinson', 'E14', 'Active', 'P23'),
('E17', 'socialmedia@company.com', 'Alex Smith', 'E14', 'Active', 'P24');

-- Developers and Analysts (with team assignments)
INSERT INTO employee (emid, email, name, reportsTo, status, positionId) VALUES
('E18', 'leaddev@company.com', 'Jordan Lee', 'E5', 'Active', 'P17'),
('E19', 'qaengineer@company.com', 'Morgan Bailey', 'E5', 'Active', 'P18'),
('E20', 'appdev@company.com', 'Taylor Green', 'E18', 'Active', 'P25'),
('E21', 'digitalmarkanalyst@company.com', 'Robin Torres', 'E14', 'Active', 'P26');

