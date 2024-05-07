using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;
using Microsoft.AspNetCore.Mvc;

using OrganizationChartMIS.Data.Service.Department;
using OrganizationChartMIS.Data.Service.Employee;
using OrganizationChartMIS.Data.Service.Position;
using OrganizationChartMIS.Data.Service.Team;
using Kendo.Mvc.UI;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public partial class DashboardModel : PageModel
    {
        // Service interfaces
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        private readonly ITeamService _teamService;

        // Data to be displayed on the page

        [BindProperty]
        public IList<Employee> Employees { get; set; }
        
        [BindProperty]
        public IList<Position> Positions { get; set; }

        [BindProperty]
        public IList<Department> Departments { get; set; }

        [BindProperty]
        public IList<Team> Teams { get; set; }

        [BindProperty]
        public Employee NewEmployee { get; set; } = new Employee();

        [BindProperty]
        public Position NewPosition { get; set; } = new Position();

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string SupervisorName { get; set; }

        [BindProperty]
        public string PositionId { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public string DepartmentId { get; set; }

        [BindProperty]
        public string TeamId { get; set; }

        [BindProperty]
        public string PositionLevel { get; set; }

        [BindProperty]
        public string DepartmentName { get; set; }

        [BindProperty]
        public string TeamName { get; set; }


        public DashboardModel(
            IEmployeeService employeeService,
            IPositionService positionService,
            IDepartmentService departmentService,
            ITeamService teamService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _positionService = positionService ?? throw new ArgumentNullException(nameof(positionService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));

        }

        public void OnGet()
        {
            Console.WriteLine("OnGet - Fetching positions, employees, departments, teams");

            Employees = _employeeService.GetAllEmployees();
            Positions = _positionService.GetAllPositions();
            Departments = _departmentService.GetAllDepartments();
            Teams = _teamService.GetAllTeams();

            Console.WriteLine($"OnGet - Fetched {Positions.Count} positions, {Employees.Count} employees, {Departments.Count} departments, {Teams.Count} Teams");
        }





    }
}
