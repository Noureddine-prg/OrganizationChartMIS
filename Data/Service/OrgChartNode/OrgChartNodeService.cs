using OrganizationChartMIS.Data.Repositories.OrgChartNode;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;


namespace OrganizationChartMIS.Data.Service.OrgChartNode
{
    public class OrgChartNodeService: IOrgChartNodeService
    {

        private readonly IOrgChartNodeRepository _orgChartNodeRepository;

        public OrgChartNodeService(IOrgChartNodeRepository orgChartNodeRepository) 
        {
            _orgChartNodeRepository = orgChartNodeRepository;
        }

        public OrgChartNodeObject CreateAndSaveNode(string positionId, string employeeId, string teamId, string reportsToNodeId) 
        {

            try 
            {
                string nodeId = GenerateNodeId();
                var node = new OrgChartNodeObject()
                {
                    NodeId = nodeId,
                    PositionId = positionId,
                    EmployeeId = employeeId,
                    TeamId = teamId,
                    ReportsToNodeId = reportsToNodeId
                };

                Console.WriteLine($"Creating Node: NodeId={nodeId}, PositionId={positionId}, EmployeeId={employeeId}, TeamId={teamId}, ReportsToNodeId={reportsToNodeId}");

                _orgChartNodeRepository.AddNode(node);

                Console.WriteLine($"Node Created: {nodeId}");
         
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
            return _orgChartNodeRepository.GetNode(nodeId);

        }

        public void UpdateNode(OrgChartNodeObject node) 
        {
            try 
            {
                OrgChartNodeObject exisitingNode = _orgChartNodeRepository.GetNode(node.NodeId);

                if (exisitingNode != null)
                {
                    exisitingNode.PositionId = node.PositionId;
                    exisitingNode.EmployeeId = node.EmployeeId;
                    exisitingNode.TeamId = node.TeamId;
                    exisitingNode.ReportsToNodeId = node.ReportsToNodeId;

                    _orgChartNodeRepository.UpdateNode(exisitingNode);
                    Console.WriteLine($"Node Updated: {node.NodeId}");

                }
                else
                {
                    Console.WriteLine("Update Node- Node not found ");
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
                    Console.WriteLine("Node has been deleted.");
                }
                else
                {
                    Console.WriteLine("Node not found. ");
                }
            } 
            catch (Exception ex) 
            {
                Console.WriteLine("Unable to find node.");
                Console.WriteLine($"DeleteNode - Exception: {ex.Message}");
            }
            
        }

        public List<OrgChartNodeObject> GetAllNodes() 
        {
            return _orgChartNodeRepository.GetAllNodes();
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
                Console.WriteLine($"Employee, {employeeId}, could not be assigned to Node {nodeId}");
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
                Console.WriteLine($" RemoveEmployeeFRomNode - Exception: {ex.Message}");
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
