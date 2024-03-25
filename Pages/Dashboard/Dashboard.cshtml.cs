using OrganizationChartMIS.Data.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationChartMIS.Data.Models;
using OrganizationChartMIS.Data.Factories;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationChartMIS.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly PositionRepository _positionRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeFactory _employeeFactory;

        public IList<Position> Positions { get; set; }
        public IList<Employee> Employees { get; set; }
        public Employee NewEmployee { get; set; } = new Employee();
        public Position NewPosition { get; set; } = new Position();

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
        public DashboardModel(PositionRepository positionRepository, EmployeeRepository employeeRepository)
        {
            _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
           

            Positions = new List<Position>();
            Employees = new List<Employee>();
            _employeeFactory = new EmployeeFactory(_employeeRepository);
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

        public void OnGet()
        {
            Console.WriteLine("OnGet - Fetching positions and employees");

            Positions = _positionRepository.GetAllPositions() ?? new List<Position>();

            Employees = _employeeRepository.GetAllEmployees() ?? new List<Employee>();

            Console.WriteLine($"OnGet - Fetched {Positions.Count} positions and {Employees.Count} employees");
        }

    }
}
