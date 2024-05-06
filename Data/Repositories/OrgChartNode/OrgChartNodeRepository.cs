using OrganizationChartMIS.Data.Context;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;


namespace OrganizationChartMIS.Data.Repositories.OrgChartNode
{
    public class OrgChartNodeRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public OrgChartNodeRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<OrgChartNodeObject> GetAllNodes() { return null; }
        public OrgChartNodeObject GetNodeById(int nid) { return null; }
        
        
        public void AddNode(OrgChartNodeObject node) 
        {
            string commandText = "";
            var parameters = new Dictionary<string, object>
            {
                //{"@NodeId", node.NodeId ?? GenerateNodeId()},
                {"@PositionId", node.PositionId},
                {"@EmployeeId", node.EmployeeId},
                {"@TeamId", node.TeamId},
                {"@ReportsToNodeId", node.ReportsToNodeId}
            };
        }

        public void UpdateNode(OrgChartNodeObject node) { }
        public void DeleteNode(int nid) { }
    }
}
