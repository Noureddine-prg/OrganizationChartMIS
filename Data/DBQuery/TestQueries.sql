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
