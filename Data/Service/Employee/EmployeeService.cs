using OrganizationChartMIS.Data.Repositories.Employee;
using EmployeeObject = OrganizationChartMIS.Data.Models.Employee;

namespace OrganizationChartMIS.Data.Service.Employee
{
    public class EmployeeService : IEmployeeService
    {

        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeObject CreateAndSaveEmployee(string Email, string Name, string Status, string PositionId)
        {
            Console.WriteLine($"CreateAndSaveEmployee - Creating Employee: Email={Email}, Name={Name}, Status={Status}, PositionId={PositionId}");

            string Emid = GenerateUniqueEmid();
            Console.WriteLine($"CreateAndSaveEmployee - Generated EMID: {Emid}");

            var newEmployee = new EmployeeObject
            {
                Emid = Emid,
                Email = Email,
                Name = Name,
                ReportsTo = null,
                Status = Status,
                PositionId = PositionId
            };

            Console.WriteLine(newEmployee.ToString());

            _employeeRepository.AddEmployee(newEmployee);
            Console.WriteLine($"CreateAndSaveEmployee - Employee Added: {newEmployee.Emid}");

            return newEmployee;
        }

        public EmployeeObject GetEmployee(string emid)
        {
            return _employeeRepository.GetEmployee(emid);
        }

        public EmployeeObject UpdateEmployee(string emid, string email, string name, string status, string positionId)
        {
            try
            {
                var employeeToUpdate = _employeeRepository.GetEmployee(emid);

                if (employeeToUpdate != null)
                {
                    employeeToUpdate.Email = email;
                    employeeToUpdate.Name = name;
                    employeeToUpdate.Status = status;
                    employeeToUpdate.PositionId = positionId;
                    _employeeRepository.UpdateEmployee(employeeToUpdate);
                    return employeeToUpdate;
                }
                else
                {
                    Console.WriteLine("UpdateEmployee - Employee not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateEmployee - Exception: {ex.Message}");
                return null;
            }
        }

        public bool DeleteEmployee(string emid)
        {
            try
            {
                var employeeToDelete = _employeeRepository.GetEmployee(emid);
                if (employeeToDelete != null)
                {
                    Console.WriteLine("DeleteEmployee - Employee set to inactive");
                    _employeeRepository.DeleteEmployee(emid);
                    return true;
                }
                else
                {
                    Console.WriteLine("DeleteEmployee - Employee not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteEmployee - Exception: {ex.Message}");
                return false;
            }
        }

        public List<EmployeeObject> GetAllEmployees()
        {
            try
            {
                return _employeeRepository.GetAllEmployees().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllEmployees - Exception: {ex.Message}");
                return new List<EmployeeObject>();
            }
        }

        public string GenerateUniqueEmid()
        {

            bool exists;
            string emid;

            do
            {
                var randomNumber = new Random().Next(100000, 999999);
                emid = $"E{randomNumber}";
                exists = _employeeRepository.CheckEmidExists(emid);
            }
            while (exists);

            return emid;
        }
    }
}
