using DepartmentObject = OrganizationChartMIS.Data.Models.Department;

namespace OrganizationChartMIS.Data.Service.Department
{
    public interface IDepartmentService
    {
        DepartmentObject CreateAndSaveDepartment(string name, string reportsTo = null);
        DepartmentObject GetDepartment(string doid);
        DepartmentObject UpdateDepartment(string doid, string name, string reportsTo = null);
        bool DeleteDepartment(string doid);

        public List<DepartmentObject> GetAllDepartments();
        public string GenerateUniqueDoid();
    }
}
