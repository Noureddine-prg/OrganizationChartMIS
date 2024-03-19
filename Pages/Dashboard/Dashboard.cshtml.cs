using OrganizationChartMIS.Data.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;

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

        public void OnGet()
        {
            Positions = _positionRepository.GetAllPositions();
            Employees = _employeeRepository.GetAllEmployees();  
        }
    }
}
