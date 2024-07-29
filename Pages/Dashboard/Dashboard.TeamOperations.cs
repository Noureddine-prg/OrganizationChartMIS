using Microsoft.AspNetCore.Mvc;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public partial class DashboardModel
    {
        public JsonResult OnGetTeams()
        {
            var teams = _teamService.GetAllTeams();
            return new JsonResult(teams);
        }
    }
}
