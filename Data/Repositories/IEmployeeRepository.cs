using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Data.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(string employeeID);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(string employeeID);
    }
}
