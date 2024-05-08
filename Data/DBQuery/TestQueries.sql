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
