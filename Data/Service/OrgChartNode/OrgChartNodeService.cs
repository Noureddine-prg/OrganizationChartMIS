using OrganizationChartMIS.Data.Repositories.OrgChartNode;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;
using DepartmentObject = OrganizationChartMIS.Data.Models.Department;
using PositionObject = OrganizationChartMIS.Data.Models.Position;
using EmployeeObject = OrganizationChartMIS.Data.Models.Employee;
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
                var position = _positionService.GetPosition(positionId);
                var department = _departmentService.GetDepartment(position?.DepartmentId);
                var node = new OrgChartNodeObject
                {
                    NodeId = nodeId,
                    PositionId = positionId,
                    EmployeeId = employeeId,
                    TeamId = teamId,
                    ReportsToNodeId = reportsToNodeId,
                    PositionName = position?.Name,
                    EmployeeName = _employeeService.GetEmployee(employeeId)?.Name,
                    EmployeeEmail = _employeeService.GetEmployee(employeeId)?.Email,
                    DepartmentName = department?.Name
                };

                _orgChartNodeRepository.AddNode(node);

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
            }
            return node;
        }

        public List<OrgChartNodeObject> GetAllNodes()
        {
            var nodes = _orgChartNodeRepository.GetAllNodes();
            foreach (var node in nodes)
            {
                PopulateNodeDetails(node);
            }
            return nodes;
        }

        public List<PositionObject> GetPositionsByDepartment(string departmentId)
        {
            return _positionService.GetPositionsByDepartment(departmentId);
        }

        public List<EmployeeObject> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        private void PopulateNodeDetails(OrgChartNodeObject node)
        {
            var position = _positionService.GetPosition(node.PositionId);
            var employee = _employeeService.GetEmployee(node.EmployeeId);
            var department = _departmentService.GetDepartment(position?.DepartmentId);

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
                _orgChartNodeRepository.DeleteNode(nodeId);
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
