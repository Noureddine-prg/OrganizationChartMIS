using Microsoft.AspNetCore.Mvc;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public partial class DashboardModel
    {
        public JsonResult OnGetPositions(string department)
        {
            var positions = _positionService.GetPositionsByDepartment(department);
            return new JsonResult(positions);
        }



    }
}
