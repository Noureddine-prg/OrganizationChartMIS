using Microsoft.AspNetCore.Mvc;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public partial class DashboardModel
    {
        public JsonResult OnGetDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            return new JsonResult(departments);
        }
    }
}
