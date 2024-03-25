using Microsoft.CodeAnalysis.CSharp.Syntax;
using OrganizationChartMIS.Data.Models;
using OrganizationChartMIS.Data.Repositories;


namespace OrganizationChartMIS.Data.Factories
{
    public class EmployeeFactory
    {

        private readonly EmployeeRepository _employeeRepository;

        public EmployeeFactory(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee CreateAndSaveEmployee(string Email, string Name, string ParentId, string Status, string PositionId) 
        {

            Console.WriteLine($"CreateAndSaveEmployee - Creating Employee: Email={Email}, Name={Name}, ParentId={ParentId}, Status={Status}, PositionId={PositionId}");

            string Emid = GenerateUniqueEmid();
            Console.WriteLine($"CreateAndSaveEmployee - Generated EMID: {Emid}");

            Employee newEmployee = new Employee()
            {
                Emid = Emid,
                Email = Email,
                Name = Name,
                ParentId = ParentId,
                Status = Status,
                PositionId = PositionId
            };

            Console.WriteLine(newEmployee.ToString());

            _employeeRepository.AddEmployee(newEmployee);
            Console.WriteLine($"CreateAndSaveEmployee - Employee Added: {newEmployee.Emid}");

            return newEmployee;

        }

        public string GenerateUniqueEmid() 
        {

            bool exists = true;
            string emid = "";

            while (exists) { 
                var randomNumber = new Random().Next(100000,999999);
                emid = $"E{randomNumber}";

                exists = _employeeRepository.CheckEmidExists(emid); 

            }

            return emid;
        }





    }
}
