using Microsoft.AspNetCore.Mvc;
using OrganizationChartMIS.Data.Service.OrgChartNode;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;

namespace OrganizationChartMIS.Pages.OrganizationChart
{
    public class OrganizationChartModel : PageModel
    {
        private readonly IOrgChartNodeService _orgChartNodeService;
        public List<OrgChartNodeObject> Nodes { get; set; } 

        public OrganizationChartModel(IOrgChartNodeService orgChartNodeService) 
        {
            _orgChartNodeService = orgChartNodeService; 
        }

        public void OnGet()
        {


        }

        public JsonResult OnGetOrgChartData()
        {
            var nodes = _orgChartNodeService.GetAllNodes();

            foreach (var node in nodes)
            {
                Console.WriteLine($"NodeId={node.NodeId}, PositionId={node.PositionId}, EmployeeId={node.EmployeeId}, TeamId={node.TeamId}, ReportsToNodeId={node.ReportsToNodeId}, PositionName={node.PositionName}, EmployeeName={node.EmployeeName}, EmployeeEmail={node.EmployeeEmail}, DepartmentName={node.DepartmentName}");
            }

            return new JsonResult(nodes);
        }

    }
}
