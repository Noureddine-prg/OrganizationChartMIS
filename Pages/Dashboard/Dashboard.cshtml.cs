using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;
using Microsoft.AspNetCore.Mvc;

using OrganizationChartMIS.Data.Service.Department;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        private readonly ITeamService _teamService;

        public IList<Employee> Employees { get; set; }
        public IList<Position> Positions { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<Team> Teams { get; set; }



        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string SupervisorName { get; set; }

        [BindProperty]
        public string Position {  get; set; }

        [BindProperty]
        public string Status {  get; set; }

        // Dependency injection
        public DashboardModel(
            IEmployeeService employeeService,
            IPositionService positionService,
            IDepartmentService departmentService,
            ITeamService teamService)
        {
            _employeeService = employeeService;
            _positionService = positionService;
            _departmentService = departmentService;
            _teamService = teamService;
        }

        public void OnGet()
        {
            Console.WriteLine("OnGet - Fetching positions and employees");

            Positions = _positionRepository.GetAllPositions();
            Employees = _employeeRepository.GetAllEmployees();
            Departments = _departmentRepository.GetAllDepartments();
            Teams = _teamRepository.GetAllTeams();

            Console.WriteLine($"OnGet - Fetched {Positions.Count} positions and {Employees.Count} employees");
        }

        public JsonResult OnGetDepartments() 
        {
            var departments = _departmentRepository.GetAllDepartments();

            return new JsonResult(departments); 
        }

        public JsonResult OnGetTeams()
        {
            var teams = _teamRepository.GetAllTeams();

            return new JsonResult(teams);
        }

        public JsonResult OnGetPositions(string department) 
        {
            var positions = _positionRepository.GetPositionsByDepartment(department);

            return new JsonResult(positions);
        }

        public JsonResult OnGetSupervisors(string department) 
        {
            var supervisors = _employeeRepository.GetAllSupervisorsByDepartment(department);

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
