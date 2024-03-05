using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees() 
        {
            List<Employee> employees = new List<Employee>();

            return employees;
        }

        public Employee GetEmployee(string employeeID) 
        {
            Employee employee = new Employee();

            return employee;
        }
        public void AddEmployee(Employee employee) { }
        public void UpdateEmployee(Employee employee) { }
        public void DeleteEmployee(string employeeID) { }
    }
}
