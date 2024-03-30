using EmployeeObject = OrganizationChartMIS.Data.Models.Employee;

namespace OrganizationChartMIS.Data.Repositories.Employee
{
    public interface IEmployeeRepository
    {
        EmployeeObject GetEmployee(string emid);
        void AddEmployee(EmployeeObject employee);
        void UpdateEmployee(EmployeeObject employee);
        void DeleteEmployee(string emid);
        List<EmployeeObject> GetAllSupervisorsByDepartment(string selectedDepartment);
        string GetSupervisorIDByNameAndEmail(string supervisorName, string email);
        bool CheckEmidExists(string emid);
    }
}
