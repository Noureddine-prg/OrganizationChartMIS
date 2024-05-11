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
            Console.WriteLine("Org Data:");
            var nodes = _orgChartNodeService.GetAllNodes();

            foreach (var node in nodes)
            {
                Console.WriteLine($"{node}");
                Console.WriteLine($"Node ID: {node.NodeId}, Reports To: {node.ReportsToNodeId}, Employee: {node.EmployeeName}, Email: {node.EmployeeEmail}");
            }

            return new JsonResult(nodes);
        }

    }
}
