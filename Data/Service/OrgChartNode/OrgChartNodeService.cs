using OrganizationChartMIS.Data.Repositories.OrgChartNode;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;
using OrganizationChartMIS.Data.Service.Position;
using OrganizationChartMIS.Data.Service.Employee;
using OrganizationChartMIS.Data.Service.Department;

namespace OrganizationChartMIS.Data.Service.OrgChartNode
{
    public class OrgChartNodeService : IOrgChartNodeService
    {
        private readonly OrgChartNodeRepository _orgChartNodeRepository;
        private readonly IPositionService _positionService;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public OrgChartNodeService(
            OrgChartNodeRepository orgChartNodeRepository,
            IPositionService positionService,
            IEmployeeService employeeService,
            IDepartmentService departmentService)
        {
            _orgChartNodeRepository = orgChartNodeRepository;
            _positionService = positionService;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public OrgChartNodeObject CreateAndSaveNode(string positionId, string employeeId, string teamId, string reportsToNodeId)
        {
            try
            {
                string nodeId = GenerateNodeId();
                var node = new OrgChartNodeObject
                {
                    NodeId = nodeId,
                    PositionId = positionId,
                    EmployeeId = employeeId,
                    TeamId = teamId,
                    ReportsToNodeId = reportsToNodeId,
                    PositionName = _positionService.GetPosition(positionId)?.Name,
                    EmployeeName = _employeeService.GetEmployee(employeeId)?.Name,
                    EmployeeEmail = _employeeService.GetEmployee(employeeId)?.Email,
                    DepartmentName = _departmentService.GetDepartment(_positionService.GetPosition(positionId)?.DepartmentId)?.Name
                };

                // So when we create and save a node, we need to give the user some options. I want to show the current positions available for the department.
                // How will we get the positions. Considering grabbing parent node, checking their department, and displaying all available positions for the department. 
                // the nodeId gets generated on its own, position Id will be set upon selection, reports to will be set on selection (Maybe get all orgnodes within thw same department) 

                Console.WriteLine($"Creating Node: NodeId={nodeId}, PositionId={positionId}, EmployeeId={employeeId}, TeamId={teamId}, ReportsToNodeId={reportsToNodeId}, PositionName={node.PositionName}, EmployeeName={node.EmployeeName}, EmployeeEmail={node.EmployeeEmail}, DepartmentName={node.DepartmentName}");

                _orgChartNodeRepository.AddNode(node);

                Console.WriteLine($"Node Created: NodeId={nodeId}, PositionName={node.PositionName}, EmployeeName={node.EmployeeName}, EmployeeEmail={node.EmployeeEmail}, DepartmentName={node.DepartmentName}");

                return node;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateAndSaveNode Exception - Node not created {ex.Message}");
                return null;
            }
        }

        public OrgChartNodeObject GetNodeById(string nodeId)
        {
            var node = _orgChartNodeRepository.GetNode(nodeId);
            if (node != null)
            {
                PopulateNodeDetails(node);
                Console.WriteLine($"Retrieved Node: NodeId={node.NodeId}, PositionName={node.PositionName}, EmployeeName={node.EmployeeName}, EmployeeEmail={node.EmployeeEmail}, DepartmentName={node.DepartmentName}");
            }
            return node;
        }

        public List<OrgChartNodeObject> GetAllNodes()
        {
            var nodes = _orgChartNodeRepository.GetAllNodes();
            foreach (var node in nodes)
            {
                PopulateNodeDetails(node);
                Console.WriteLine($"Retrieved Node: NodeId={node.NodeId}, PositionName={node.PositionName}, EmployeeName={node.EmployeeName}, EmployeeEmail={node.EmployeeEmail}, DepartmentName={node.DepartmentName}");
            }
            return nodes;
        }

        private void PopulateNodeDetails(OrgChartNodeObject node)
        {
            var position = _positionService.GetPosition(node.PositionId);
            var employee = _employeeService.GetEmployee(node.EmployeeId);
            var department = position != null ? _departmentService.GetDepartment(position.DepartmentId) : null;

            node.PositionName = position?.Name;
            node.EmployeeName = employee?.Name;
            node.EmployeeEmail = employee?.Email;
            node.DepartmentName = department?.Name;
        }

        public void UpdateNode(OrgChartNodeObject node)
        {
            try
            {
                OrgChartNodeObject existingNode = _orgChartNodeRepository.GetNode(node.NodeId);

                if (existingNode != null)
                {
                    existingNode.PositionId = node.PositionId;
                    existingNode.EmployeeId = node.EmployeeId;
                    existingNode.TeamId = node.TeamId;
                    existingNode.ReportsToNodeId = node.ReportsToNodeId;

                    PopulateNodeDetails(existingNode);

                    _orgChartNodeRepository.UpdateNode(existingNode);
                    Console.WriteLine($"Node Updated: NodeId={existingNode.NodeId}, PositionName={existingNode.PositionName}, EmployeeName={existingNode.EmployeeName}, EmployeeEmail={existingNode.EmployeeEmail}, DepartmentName={existingNode.DepartmentName}");
                }
                else
                {
                    Console.WriteLine("Update Node - Node not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Node Exception: {ex.Message}");
            }
        }

        public void DeleteNode(string nodeId)
        {
            try
            {
                OrgChartNodeObject node = _orgChartNodeRepository.GetNode(nodeId);

                if (node != null)
                {
                    _orgChartNodeRepository.DeleteNode(nodeId);
                    Console.WriteLine($"Node Deleted: NodeId={nodeId}");
                }
                else
                {
                    Console.WriteLine("Node not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteNode Exception: {ex.Message}");
            }
        }

        public void AssignEmployeeToNode(string nodeId, string employeeId)
        {
            try
            {
                _orgChartNodeRepository.AssignEmployeeToNode(nodeId, employeeId);

                Console.WriteLine($"Employee {employeeId} assigned to Node {nodeId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AssignEmployee - Exception: {ex.Message}");
            }
        }

        public void RemoveEmployeeFromNode(string nodeId)
        {
            try
            {
                _orgChartNodeRepository.RemoveEmployeeFromNode(nodeId);

                Console.WriteLine($"Employee Removed from {nodeId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RemoveEmployeeFromNode - Exception: {ex.Message}");
            }
        }

        public string GenerateNodeId()
        {
            bool exists;
            string nodeId;

            do
            {
                nodeId = $"N{new Random().Next(100000, 999999)}";
                exists = _orgChartNodeRepository.CheckNodeIdExists(nodeId);
            }
            while (exists);

            return nodeId;
        }
    }
}
