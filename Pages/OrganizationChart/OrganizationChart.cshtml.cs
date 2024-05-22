using Microsoft.AspNetCore.Mvc;
using OrganizationChartMIS.Data.Service.OrgChartNode;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;

using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Pages.OrganizationChart
{
    public class OrganizationChartModel : PageModel
    {


        private readonly IOrgChartNodeService _orgChartNodeService;
        public List<OrgChartNodeObject> Nodes { get; set; }
        
        [BindProperty]
        public OrgChartNodeObject NewNode { get; set; } = new OrgChartNodeObject();

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

            return new JsonResult(nodes);
        }

        public JsonResult OnGetNode(string nodeId)
        {
            var node = _orgChartNodeService.GetNodeById(nodeId);
            return new JsonResult(node);
        }

        public JsonResult OnGetPositionsByDepartment(string departmentId)
        {
            var positions = _orgChartNodeService.GetPositionsByDepartment(departmentId);
            return new JsonResult(positions);
        }

        public IActionResult OnPostCreateNode([FromBody] OrgChartNodeObject newNode)
        {
            var createdNode = _orgChartNodeService.CreateAndSaveNode(
                newNode.PositionId, newNode.EmployeeId, newNode.TeamId, newNode.ReportsToNodeId);

            if (createdNode != null)
            {
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }



    }
}
