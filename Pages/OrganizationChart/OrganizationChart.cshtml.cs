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

        public JsonResult OnGetOrgChartNodes()
        {
            Console.WriteLine("We are grabbing");
            var nodes = _orgChartNodeService.GetAllNodes();

            foreach (var node in nodes)
            {
                Console.WriteLine($"Node ID: {node.NodeId}, Position: {node.PositionName}, Employee: {node.EmployeeName}, Email: {node.EmployeeEmail}");
            }

            return new JsonResult(nodes);
        }

    }
}
