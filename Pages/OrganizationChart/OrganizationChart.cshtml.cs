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
            Nodes = _orgChartNodeService.GetAllNodes();
            Console.WriteLine(Nodes[1].ToString());
        }
    }
}
