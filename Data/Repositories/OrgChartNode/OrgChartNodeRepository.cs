using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure.Implementation;
using Microsoft.Extensions.Configuration;
using OrganizationChartMIS.Data.Context;
using OrganizationChartMIS.Data.Models;
using System.Data;
using System.Transactions;
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

        public List<OrgChartNodeObject> GetAllNodes() 
        {
            List<OrgChartNodeObject> nodes = new List<OrgChartNodeObject>();

            string commandText = @"
            SELECT 
                n.nodeId, 
                n.positionId, 
                n.employeeId, 
                n.teamId, 
                n.reportsToNodeId,
                p.name AS PositionName,
                e.name AS EmployeeName, 
                e.email AS EmployeeEmail,
                t.teamName AS TeamName
            FROM orgnode n
            LEFT JOIN position p ON n.positionId = p.poid
            LEFT JOIN employee e ON n.employeeId = e.emid
            LEFT JOIN team t ON n.teamId = t.teamid";

            DataTable dataTable = _databaseHelper.ExecuteQuery(commandText);

            foreach (DataRow row in dataTable.Rows) 
            {
                nodes.Add(new OrgChartNodeObject {
                    NodeId = row["nodeId"].ToString(),
                    PositionId = row["positionId"].ToString(),
                    EmployeeId = row.IsNull("employeeId") ? null : row["employeeId"].ToString(),
                    TeamId = row.IsNull("teamId") ? null : row["teamId"].ToString(),
                    ReportsToNodeId = row.IsNull("reportsToNodeId") ? null : row["reportsToNodeId"].ToString(),
                    PositionName = row.IsNull("PositionName") ? null : row["PositionName"].ToString(),
                    EmployeeName = row.IsNull("EmployeeName") ? null : row["EmployeeName"].ToString(),
                    EmployeeEmail = row.IsNull("EmployeeEmail") ? null : row["EmployeeEmail"].ToString(),
                    DepartmentName = row.IsNull("DepartmentName") ? null : row["DepartmentName"].ToString()
                });
            }

            return nodes;
        }
        
        public void AddNode(OrgChartNodeObject node)
        {
            string commandText = @"
            INSERT INTO orgnode (nodeId, positionId, employeeId, teamId, reportsToNodeId)
            VALUES (@NodeId, @PositionId, @EmployeeId, @TeamId, @ReportsToNodeId)";

            var parameters = new Dictionary<string, object>
            {
                {"@NodeId", node.NodeId},
                {"@PositionId", node.PositionId},
                {"@EmployeeId", node.EmployeeId},
                {"@TeamId", node.TeamId},
                {"@ReportsToNodeId", node.ReportsToNodeId}
            };

            _databaseHelper.ExecuteQuery(commandText, parameters);  
        }

        public OrgChartNodeObject GetNode(string nodeId)
        {
            string commandText = @"
            SELECT n.nodeId, n.positionId, n.employeeId, n.teamId, n.reportsToNodeId,
                   p.name AS PositionName,
                   e.name AS EmployeeName, e.email AS EmployeeEmail,
                   d.name AS DepartmentName
            FROM orgnode n
            LEFT JOIN position p ON n.positionId = p.poid
            LEFT JOIN employee e ON n.employeeId = e.emid
            LEFT JOIN department d ON p.departmentId = d.doid
            WHERE n.nodeId = @NodeId";

            var parameters = new Dictionary<string, object> { { "@NodeId", nodeId } };
            var dataTable = _databaseHelper.ExecuteQuery(commandText, parameters);

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                return new OrgChartNodeObject
                {
                    NodeId = row["nodeId"].ToString(),
                    PositionId = row["positionId"].ToString(),
                    EmployeeId = row.IsNull("employeeId") ? null : row["employeeId"].ToString(),
                    TeamId = row.IsNull("teamId") ? null : row["teamId"].ToString(),
                    ReportsToNodeId = row.IsNull("reportsToNodeId") ? null : row["reportsToNodeId"].ToString(),
                    PositionName = row.IsNull("PositionName") ? null : row["PositionName"].ToString(),
                    EmployeeName = row.IsNull("EmployeeName") ? null : row["EmployeeName"].ToString(),
                    EmployeeEmail = row.IsNull("EmployeeEmail") ? null : row["EmployeeEmail"].ToString(),
                    DepartmentName = row.IsNull("DepartmentName") ? null : row["DepartmentName"].ToString()
                };
            }

            return null;
        }

        public void UpdateNode(OrgChartNodeObject node)
        {
            string commandText = @"
            UPDATE orgnode
            SET positionId = @PositionId, employeeId = @EmployeeId, teamId = @TeamId, reportsToNodeId = @ReportsToNodeId
            WHERE nodeId = @NodeId";

            var parameters = new Dictionary<string, object>
            {
                { "@NodeId", node.NodeId },
                { "@PositionId", node.PositionId},
                { "@EmployeeId", node.EmployeeId  ?? (object)DBNull.Value},
                { "@TeamId", node.TeamId ?? (object)DBNull.Value},
                { "@ReportsTo", node.ReportsToNodeId ??(object) DBNull.Value}
            };

            _databaseHelper.ExecuteQuery(commandText, parameters);  

        }

        public void DeleteNode(string nodeId)
        {
            string commandText = "DELETE FROM orgnode WHERE nodeId = @NodeId";
            var parameters = new Dictionary<string, object> { { "@NodeId", nodeId } };

            _databaseHelper.ExecuteUpdate(commandText, parameters);
        }

        public void AssignEmployeeToNode(string nodeId, string employeeId)
        {

            string updateEmployeeQuery = "UPDATE employee SET positionId = (SELECT positionId FROM orgnode WHERE nodeId = @NodeId) WHERE emid = @EmployeeId";

            var employeeParam = new Dictionary<string, object>
            {
                { "@NodeId", nodeId },
                { "@EmployeeId", employeeId}
            };

            _databaseHelper.ExecuteQuery(updateEmployeeQuery, employeeParam);

            string updateNodeQuery = "UPDATE orgnode SET employeeId = @EmployeeId WHERE nodeId = @NodeId";
            
            var nodeParam = new Dictionary<string, object>
            {
                { "@NodeId", nodeId },
                { "@EmployeeId", employeeId }
            };

            _databaseHelper.ExecuteQuery(updateNodeQuery, nodeParam);

        }

        public void RemoveEmployeeFromNode(string nodeId) 
        {
            string commandText = "UPDATE orgnode SET employeeId = NULL WHERE nodeId = @NodeId";
            _databaseHelper.ExecuteUpdate(commandText, new Dictionary<string, object> { { "@NodeId", nodeId } });
        }

        public bool CheckNodeIdExists(string nodeId)
        {
            string commandText = "SELECT COUNT(*) FROM orgnode WHERE nodeId = @NodeId";
            var parameters = new Dictionary<string, object> { { "@NodeId", nodeId } };

            var dataTable = _databaseHelper.ExecuteQuery(commandText, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }

            return false;
        }
    }
}
