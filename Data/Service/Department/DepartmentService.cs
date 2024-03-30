using OrganizationChartMIS.Data.Repositories.Department;
using DepartmentObject = OrganizationChartMIS.Data.Models.Department;

namespace OrganizationChartMIS.Data.Service.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public DepartmentObject CreateAndSaveDepartment(string name, string reportsTo = null)
        {
            var doid = GenerateUniqueDoid();
            Console.WriteLine($"CreateAndSaveDepartment - Creating Department: Name={name}, ReportsTo={reportsTo}, DOID={doid}");

            var newDepartment = new DepartmentObject
            {
                Doid = doid,
                Name = name,
                ReportsTo = reportsTo
            };

            _departmentRepository.AddDepartment(newDepartment);
            Console.WriteLine($"CreateAndSaveDepartment - Department Added: {newDepartment.Name}");

            return newDepartment;
        }

        public DepartmentObject GetDepartment(string doid)
        {
            try
            {
                return _departmentRepository.GetDepartment(doid);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDepartment - Exception: {ex.Message}");
                return null;
            }
        }

        public DepartmentObject UpdateDepartment(string doid, string name, string reportsTo = null)
        {
            try
            {
                var departmentToUpdate = _departmentRepository.GetDepartment(doid);

                if (departmentToUpdate != null)
                {
                    departmentToUpdate.Doid = doid;
                    departmentToUpdate.Name = name;
                    departmentToUpdate.ReportsTo = reportsTo;
                    _departmentRepository.UpdateDepartment(departmentToUpdate);
                    return departmentToUpdate;
                }
                else
                {
                    Console.WriteLine("UpdateDepartment - Department not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateDepartment - Exception: {ex.Message}");
                return null;
            }
        }

        public bool DeleteDepartment(string doid)
        {
            try
            {
                var departmentToDelete = _departmentRepository.GetDepartment(doid);
                if (departmentToDelete != null)
                {
                    _departmentRepository.DeleteDepartment(doid);
                    return true;
                }
                else
                {
                    Console.WriteLine("DeleteDepartment - Department not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteDepartment - Exception: {ex.Message}");
                return false;
            }
        }

        public List<DepartmentObject> GetAllDepartments()
        {
            try
            {
                return _departmentRepository.GetAllDepartments().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllDepartments - Exception: {ex.Message}");
                return new List<DepartmentObject>();
            }
        }


        //doid generation 

        public string GenerateUniqueDoid()
        {
            string doid;
            bool exists;

            do
            {
                var randomNumber = new Random().Next(100000, 999999);
                doid = $"D{randomNumber}";
                exists = _departmentRepository.CheckDoidExists(doid);
            }
            while (exists);

            return doid;
        }
    }
}
