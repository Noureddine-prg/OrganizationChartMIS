using Microsoft.Extensions.Configuration;
using OrganizationChartMIS.Data.Context;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;



namespace OrganizationChartMIS.Data.Repositories.OrgChartNode
{
    public class OrgChartNodeRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public OrgChartNodeRepository(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
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
        public void DeleteNode(string nodeId)
        {
            string sql = "DELETE FROM orgnode WHERE nodeId = @NodeId";
            var parameters = new Dictionary<string, object> { { "@NodeId", nodeId } };
            _databaseHelper.ExecuteUpdate(sql, parameters);
        }

        public void AssignEmployeeToNode(string nodeId, string employeeId) 
        {
            string sql = "UPDATE orgnode SET employeeId = @EmployeeId WHERE nodeId = @NodeId";
            _databaseHelper.ExecuteUpdate(sql, new Dictionary<string, object>
            {
                {"@NodeId", nodeId},
                {"@EmployeeId", employeeId}
            });
        }

        public void RemoveEmployeeFromNode(string nodeId) 
        { 
            string sql = "UPDATE orgnode SET employeeId = NULL WHERE nodeId = @NodeId";
            _databaseHelper.ExecuteUpdate(sql, new Dictionary<string, object> { { "@NodeId", nodeId } });
        }

        public bool CheckNodeIdExists(string emid)
        {
            string query = "SELECT COUNT(*) FROM employee WHERE Emid = @Emid";
            var parameters = new Dictionary<string, object> { { "@Emid", emid } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }

            return false;
        }
    }
}
