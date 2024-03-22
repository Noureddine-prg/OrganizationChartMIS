using OrganizationChartMIS.Data.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly PositionRepository _positionRepository;
        private readonly EmployeeRepository _employeeRepository;

        public IList<Position> Positions { get; set; }
        public IList<Employee> Employees { get; set; }

        // Dependency injection
        public DashboardModel(PositionRepository positionRepository, EmployeeRepository employeeRepository)
        {
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
        }

        public JsonResult OnGetDepartments() 
        {
            var departments = _positionRepository.GetAllDepartments();

            return new JsonResult(departments); 
        }

        public JsonResult OnGetPositions(string department) 
        {
            var positions = _positionRepository.GetPositionsByDepartment(department);

            return new JsonResult(positions);
        }

        public JsonResult OnGetSupervisors(string department) 
        {
            var supervisors = _positionRepository.GetAllSupervisorsByDepartment(department);

            return new JsonResult(supervisors);
        }

        public void OnGet()
        {
            Positions = _positionRepository.GetAllPositions();
            Employees = _employeeRepository.GetAllEmployees();  
        }
    }
}
