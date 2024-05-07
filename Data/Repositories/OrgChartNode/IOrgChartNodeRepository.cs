using OrganizationChartMIS.Data.Context;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;


namespace OrganizationChartMIS.Data.Repositories.OrgChartNode
{
    public interface IOrgChartNodeRepository
    {
        List<OrgChartNodeObject> GetAllNodes();
        OrgChartNodeObject GetNode(string nodeId);
        void AddNode(OrgChartNodeObject node);
        void UpdateNode(OrgChartNodeObject node);
        void DeleteNode(string nodeId);
        void AssignEmployeeToNode(string nodeId, string employeeId);
        void RemoveEmployeeFromNode(string nodeId);
        public bool CheckNodeIdExists(string nodeId);
    }
}
