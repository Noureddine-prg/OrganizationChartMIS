
using System.Collections.Generic;
using DepartmentObject = OrganizationChartMIS.Data.Models.Department;

namespace OrganizationChartMIS.Data.Repositories.Department
{
    public interface IDepartmentRepository
    {
        DepartmentObject GetDepartment(string doid);
        void AddDepartment(DepartmentObject department);
        void UpdateDepartment(DepartmentObject department);
        void DeleteDepartment(string doid);
        List<DepartmentObject> GetAllDepartments();
        bool CheckDoidExists(string doid);
    }
}
