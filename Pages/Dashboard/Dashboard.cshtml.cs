using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;
using Microsoft.AspNetCore.Mvc;

using OrganizationChartMIS.Data.Service.Department;
using OrganizationChartMIS.Data.Service.Employee;
using OrganizationChartMIS.Data.Service.Position;
using OrganizationChartMIS.Data.Service.Team;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        // Service interfaces
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        private readonly ITeamService _teamService;

        // Data to be displayed on the page
        public IList<Employee> Employees { get; private set; }
        public IList<Position> Positions { get; private set; }
        public IList<Department> Departments { get; private set; }
        public IList<Team> Teams { get; private set; }

        // Constructor with dependency injection
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

            Console.WriteLine($"OnGet - Fetched {Positions.Count} positions, {Employees.Count} employees, {Departments.Count} departments, {Teams.Count}");
        }

        public JsonResult OnGetDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            return new JsonResult(departments);
        }

        public JsonResult OnGetTeams()
        {
            var teams = _teamService.GetAllTeams();
            return new JsonResult(teams);
        }

        public JsonResult OnGetPositions(string department)
        {
            var positions = _positionService.GetPositionsByDepartment(department);
            return new JsonResult(positions);
        }

        public JsonResult OnGetSupervisors(string department)
        {
            var supervisors = _employeeService.GetAllSupervisorsByDepartment(department);
            return new JsonResult(supervisors);
        }


        public IActionResult OnPostAddNewEmployee(string name, string email, string positionName, string supervisorName, string status)
        {
            Console.WriteLine($"OnPostAddNewEmployeeAsync - Information Name: {name}, Email: {email}, PositionName: {positionName}, SupervisorName: {supervisorName}, Status: {status}");

            try
            {
                Console.WriteLine($"OnPostAddNewEmployee - SupervisorEMID: {supervisorName}, POID: {positionName}");
                var employee = _employeeFactory.CreateAndSaveEmployee(email, name, supervisorName, status, positionName);
                Console.WriteLine($"OnPostAddNewEmployee - Employee Created: {employee.Emid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnPostAddNewEmployee - Exception: {ex.Message}");
            }

            return RedirectToPage("./Dashboard");
        }



    }
}
