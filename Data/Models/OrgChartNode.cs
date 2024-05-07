using Kendo.Mvc.UI;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;


namespace OrganizationChartMIS.Data.Models
{
    public class OrgChartNode
    {
        public string NodeId { get; set; }
        public string PositionId { get; set; }
        public string EmployeeId { get; set; }
        public string TeamId { get; set; }
        public string ReportsToNodeId { get; set; }
        //grab all org nodes, 

        //Properties to be displayed
        public string PositionName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string DepartmentName { get; set; }  
    }
        


}
