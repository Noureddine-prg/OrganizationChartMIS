INSERT INTO department (doid, name) VALUES
('D001', 'Executive'),
('D002', 'Finance'),
('D003', 'Information Technology'),
('D004', 'Operations');

INSERT INTO position (poid, name, level, departmentId) VALUES
('P001', 'CEO', 1, 'D001'),
('P002', 'CFO', 2, 'D002'),
('P003', 'CIO', 2, 'D003'),
('P004', 'COO', 2, 'D004'),
('P005', 'Finance Manager', 3, 'D002'),
('P006', 'IT Manager', 3, 'D003'),
('P007', 'Operations Manager', 3, 'D004');

INSERT INTO employee (emid, email, name, status, positionId) VALUES
('E001', 'ceo@example.com', 'John Doe', 'Active', 'P001'),
('E002', 'cfo@example.com', 'Jane Smith', 'Active', 'P002'),
('E003', 'cio@example.com', 'Alice Johnson', 'Active', 'P003'),
('E004', 'coo@example.com', 'Bob Brown', 'Active', 'P004'),
('E005', 'finmgr@example.com', 'Chris Davis', 'Active', 'P005'),
('E006', 'itmgr@example.com', 'Eva Green', 'Active', 'P006'),
('E007', 'opsmgr@example.com', 'Frank White', 'Active', 'P007');

INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N001', 'P001', 'E001', NULL, NULL),  -- CEO has no reportsTo node
('N002', 'P002', 'E002', NULL, 'N001'), -- CFO reports to CEO
('N003', 'P003', 'E003', NULL, 'N001'), -- CIO reports to CEO
('N004', 'P004', 'E004', NULL, 'N001'), -- COO reports to CEO
('N005', 'P005', 'E005', NULL, 'N002'), -- Finance Manager reports to CFO
('N006', 'P006', 'E006', NULL, 'N003'), -- IT Manager reports to CIO
('N007', 'P007', 'E007', NULL, 'N004'); -- Operations Manager reports to COO

INSERT INTO department (doid, name) VALUES
('D005', 'Marketing'),
('D006', 'Human Resources'),
('D007', 'Research and Development'),
('D008', 'Customer Service');

INSERT INTO position (poid, name, level, departmentId) VALUES
('P008', 'Marketing Director', 3, 'D005'),
('P009', 'HR Director', 3, 'D006'),
('P010', 'R&D Director', 3, 'D007'),
('P011', 'Customer Service Director', 3, 'D008');

INSERT INTO position (poid, name, level, departmentId) VALUES
('P012', 'Senior Marketing Manager', 4, 'D005'),
('P013', 'Senior HR Manager', 4, 'D006'),
('P014', 'Lead Research Scientist', 4, 'D007'),
('P015', 'Head of Customer Support', 4, 'D008');

INSERT INTO position (poid, name, level, departmentId) VALUES
('P016', 'Strategy Director', 3, 'D005');

INSERT INTO employee (emid, email, name, status, positionId) VALUES
('E008', 'marketingdirector@example.com', 'Laura Grey', 'Active', 'P008'),
('E009', 'hrdirector@example.com', 'Mohamed Al', 'Active', 'P009'),
('E010', 'rddirector@example.com', 'Liu Wei', 'Active', 'P010'),
('E011', 'csdirector@example.com', 'Sophia Johnson', 'Active', 'P011'),
('E012', 'seniormktmgr@example.com', 'Carlos Blue', 'Active', 'P012'),
('E013', 'seniorhrmgr@example.com', 'Fatima Zain', 'Active', 'P013'),
('E014', 'leadresearch@example.com', 'Anna Brown', 'Active', 'P014'),
('E015', 'headofcs@example.com', 'James Wolk', 'Active', 'P015');

-- Nodes for new positions with employees
INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N008', 'P008', 'E008', NULL, 'N002'),  -- Marketing Director reports to CFO
('N009', 'P009', 'E009', NULL, 'N003'),  -- HR Director reports to CIO
('N010', 'P010', 'E010', NULL, 'N004'),  -- R&D Director reports to COO
('N011', 'P011', 'E011', NULL, 'N004'),  -- Customer Service Director reports to COO
('N012', 'P012', NULL, NULL, 'N008'),  -- Senior Marketing Manager reports to Marketing Director
('N013', 'P013', 'E013', NULL, 'N009'),  -- Senior HR Manager reports to HR Director
('N014', 'P014', 'E014', NULL, 'N010'),  -- Lead Research Scientist reports to R&D Director
('N015', 'P015', 'E015', NULL, 'N011');  -- Head of Customer Support reports to Customer Service Director

-- Node for the position without an employee
INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N016', 'P016', NULL, NULL, 'N002');  -- Strategy Director reports to CFO without an assigned employee

-- Additional level 4 positions under Marketing
INSERT INTO position (poid, name, level, departmentId) VALUES
('P017', 'Brand Manager', 4, 'D005'),
('P018', 'Social Media Manager', 4, 'D005');

-- Additional level 4 positions under Human Resources
INSERT INTO position (poid, name, level, departmentId) VALUES
('P019', 'Recruitment Manager', 4, 'D006'),
('P020', 'Training Manager', 4, 'D006');

-- Additional level 4 positions under Research and Development
INSERT INTO position (poid, name, level, departmentId) VALUES
('P021', 'Product Development Manager', 4, 'D007'),
('P022', 'Data Scientist', 4, 'D007');

-- Additional level 4 positions under Customer Service
INSERT INTO position (poid, name, level, departmentId) VALUES
('P023', 'Technical Support Manager', 4, 'D008'),
('P024', 'Quality Assurance Manager', 4, 'D008');

INSERT INTO employee (emid, email, name, status, positionId) VALUES
('E016', 'brandmgr@example.com', 'Nina Ricci', 'Active', 'P017'),
('E017', 'socialmgr@example.com', 'Tom Hardy', 'Active', 'P018'),
('E018', 'recruitmgr@example.com', 'Helen Mirren', 'Active', 'P019'),
('E019', 'trainingmgr@example.com', 'Leonardo Caprio', 'Active', 'P020'),
('E020', 'productdevmgr@example.com', 'Morgan Freeman', 'Active', 'P021'),
('E021', 'datascientist@example.com', 'Natalie Portman', 'Active', 'P022'),
('E022', 'techsupportmgr@example.com', 'Bruce Willis', 'Active', 'P023'),
('E023', 'qamgr@example.com', 'Jennifer Lawrence', 'Active', 'P024');

-- Nodes for additional level 4 positions with employees
INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N017', 'P017', NULL, NULL, 'N008'),  -- Brand Manager under Marketing Director
('N018', 'P018', 'E017', NULL, 'N008'),  -- Social Media Manager under Marketing Director
('N019', 'P019', 'E018', NULL, 'N009'),  -- Recruitment Manager under HR Director
('N020', 'P020', NULL, NULL, 'N009'),  -- Training Manager under HR Director
('N021', 'P021', 'E020', NULL, 'N010'),  -- Product Development Manager under R&D Director
('N022', 'P022', NULL, NULL, 'N010'),  -- Data Scientist under R&D Director
('N023', 'P023', 'E022', NULL, 'N011'),  -- Technical Support Manager under Customer Service Director
('N024', 'P024', NULL, NULL, 'N011');  -- Quality Assurance Manager under Customer Service Director

-- Additional positions in IT and Finance, with some overlapping roles
INSERT INTO position (poid, name, level, departmentId) VALUES
('P025', 'Senior Software Engineer', 4, 'D003'), -- Under IT
('P026', 'Senior Software Engineer', 4, 'D003'), -- Another under IT, showing multiple engineers
('P027', 'Junior Accountant', 4, 'D002'), -- Under Finance
('P028', 'Junior Accountant', 4, 'D002'), -- Another under Finance, showing multiple accountants
('P029', 'Senior Accountant', 4, 'D002'), -- Under Finance
('P030', 'IT Support Specialist', 4, 'D003'); -- Under IT

-- Adding new employees to these positions
INSERT INTO employee (emid, email, name, status, positionId) VALUES
('E024', 'seniordev1@example.com', 'Clara Oswald', 'Active', 'P025'),
('E025', 'seniordev2@example.com', 'Danny Pink', 'Active', 'P026'),
('E026', 'junioracc1@example.com', 'Amy Pond', 'Active', 'P027'),
('E027', 'junioracc2@example.com', 'Rory Williams', 'Active', 'P028'),
('E028', 'senioracc@example.com', 'River Song', 'Active', 'P029'),
('E029', 'itsupport@example.com', 'Martha Jones', 'Active', 'P030');

-- Org nodes for these new positions, demonstrating varied reporting lines
INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N025', 'P025', 'E024', NULL, 'N006'),  -- Reports to IT Manager
('N026', 'P026', 'E025', NULL, 'N006'),  -- Another engineer reporting to the same IT Manager
('N027', 'P027', 'E026', NULL, 'N005'),  -- Junior Accountant under Finance Manager
('N028', 'P028', 'E027', NULL, 'N005'),  -- Another accountant under the same Finance Manager
('N029', 'P029', 'E028', NULL, 'N005'),  -- Senior Accountant also under Finance Manager
('N030', 'P030', 'E029', NULL, 'N006');  -- IT Support Specialist under IT Manager

-- Adding additional managerial roles to demonstrate further complexity
INSERT INTO position (poid, name, level, departmentId) VALUES
('P031', 'Lead Developer', 4, 'D003'),
('P032', 'Lead Accountant', 4, 'D002');

-- Assigning employees to these new managerial roles
INSERT INTO employee (emid, email, name, status, positionId) VALUES
('E030', 'leaddev@example.com', 'Donna Noble', 'Active', 'P031'),
('E031', 'leadacc@example.com', 'Wilfred Mott', 'Active', 'P032');

-- Org nodes for the new managerial roles
INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId) VALUES
('N031', 'P031', 'E030', NULL, 'N006'),  -- Lead Developer under IT Manager
('N032', 'P032', 'E031', NULL, 'N005');  -- Lead Accountant under Finance Manager
