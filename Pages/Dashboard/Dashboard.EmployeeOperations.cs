using Microsoft.AspNetCore.Mvc;
using OrganizationChartMIS.Data.Service.Employee;


namespace OrganizationChartMIS.Pages.Dashboard
{
    public partial class DashboardModel
    {

        public JsonResult OnGetEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return new JsonResult(employees);
        }

        public IActionResult OnPostAddNewEmployee(string name, string email, string positionId, string status)
        {
            Console.WriteLine($"OnPostAddNewEmployee - Information: Name: {name}, Email: {email}, PositionName: {positionId}, Status: {status}");

            try
            {
                var employee = _employeeService.CreateAndSaveEmployee(email, name, status, positionId);
                Console.WriteLine($"OnPostAddNewEmployee - Employee Created: {employee.Emid}");

                return RedirectToPage("./Dashboard");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnPostAddNewEmployee - Exception: {ex.Message}");
                return RedirectToPage("./Dashboard");

            }
        }

        public JsonResult OnGetEditEmployee(string emid)
        {
            NewEmployee = _employeeService.GetEmployee(emid);

            Console.WriteLine(NewEmployee.Name);
            if (NewEmployee == null)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(NewEmployee);
        }

        public IActionResult OnPostUpdateEmployee(string emid, string email, string name, string status, string positionId)
        {
            Console.WriteLine($"OnPostUpdateEmployee - Updating Employee: EMID={emid}, Name={name}, Email={email}, Status={status}, PositionID={positionId}");

            var updateResult = _employeeService.UpdateEmployee(emid, email, name, status, positionId);

            if (updateResult != null)
            {
                Console.WriteLine($"OnPostUpdateEmployee - Employee Updated: {updateResult.Emid}");
                return RedirectToPage("./Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update employee.");
                return Page();
            }
        }

        public IActionResult OnPostDeleteEmployee(string emid)
        {
            Console.WriteLine($"OnPostDeleteEmployee - Attempting to delete/deactivate Employee: EMID={emid}");

            bool result = _employeeService.DeleteEmployee(emid);
            if (result)
            {
                Console.WriteLine($"OnPostDeleteEmployee - Employee Deleted/Inactivated: {emid}");
                return RedirectToPage("./Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete/deactivate employee.");
                return Page();
            }
        }
    }
}
