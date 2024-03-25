using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(string emid);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(string emid);
        List<Employee> GetAllSupervisorsByDepartment(string selectedDepartment);
        string GetSupervisorIDByNameAndEmail(string supervisorName, string email);
        bool CheckEmidExists(string emid);
    }
}
