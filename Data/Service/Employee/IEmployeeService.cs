using EmployeeObject = OrganizationChartMIS.Data.Models.Employee;

namespace OrganizationChartMIS.Data.Service.Employee
{
    public interface IEmployeeService
    {
        EmployeeObject CreateAndSaveEmployee(string Email, string Name, string Status, string PositionId);
        EmployeeObject GetEmployee(string emid);
        EmployeeObject UpdateEmployee(string emid, string email, string name, string status, string positionId);
        bool DeleteEmployee(string emid);
        List<EmployeeObject> GetAllEmployees();
        string GetDepartmentIdForEmployee(string positionId);
        string GenerateUniqueEmid();
    }
}
