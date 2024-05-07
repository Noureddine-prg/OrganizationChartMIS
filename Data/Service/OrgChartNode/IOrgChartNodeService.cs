using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;

namespace OrganizationChartMIS.Data.Service.OrgChartNode
{
    public interface IOrgChartNodeService
    {
        OrgChartNodeObject CreateAndSaveNode(string positionId, string employeeId, string teamId, string reportsToNodeId);
        OrgChartNodeObject GetNodeById(string id);
        void UpdateNode(OrgChartNodeObject node);
        void DeleteNode(string nodeId);

        List<OrgChartNodeObject> GetAllNodes();
        void AssignEmployeeToNode(string nodeId, string employeeId);
        void RemoveEmployeeFromNode(string nodeId);
        string GenerateNodeId();
    }
}
