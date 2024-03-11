using OrganizationChartMIS.Data.DatabaseHelper;
using OrganizationChartMIS.Data.Models;
using System.Data;

namespace OrganizationChartMIS.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public EmployeeRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }


        public List<Employee> GetAllEmployees() 
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT EmployeeID, FirstName, LastName, Email, PositionID";

            DataTable dataTable = _databaseHelper.ExecuteQuery(query);
            
            foreach (DataRow row in dataTable.Rows) 
            {
                employees.Add(new Employee
                {
                    EmployeeID = row["EmployeeID"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    PositionID = row["PositionID"].ToString()
                }); 
            }

            return employees;
        }

        public Employee GetEmployee(string employeeID) 
        {
            Employee employee = null;
            string query = "SELECT EmployeeID, FirstName, LastName, Email, PositionID FROM Employees WHERE EmployeeID = @EmployeeID";

            var parameters = new Dictionary<string, object> { { "@EmployeeID", employeeID } };
            
            DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);
            DataRow  row = dataTable.Rows[0];

            if (dataTable.Rows.Count > 0) {
                employee = new Employee
                {
                    EmployeeID = row["EmployeeID"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    PositionID = row["PositionID"].ToString()
                };
            }

            return employee;
        }
        public void AddEmployee(Employee employee) {
            string query = "INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, PositionID) " +
                "VALUES (@EmployeeID, @FirstName, @LastName, @Email, @PositionID)";

            var parameters = new Dictionary<string, object>
            {
                { "@EmployeeID", employee.EmployeeID },
                { "@FirstName", employee.FirstName},
                { "@LastName", employee.LastName},
                { "@Email", employee.Email},
                { "@PositionID", employee.PositionID }
            };
        }
        public void UpdateEmployee(Employee employee) {
            string query = @"UPDATE Employees 
            SET FirstName=@FirstName, 
            LastName=@LastName,
            Email=@Email,
            PositionID=@PositionID
            WHERE EmployeeID = @EmployeeID";

            var parameters = new Dictionary<string, object> {
                { "@EmployeeID", employee.EmployeeID },
                { "@FirstName", employee.FirstName},
                { "@LastName", employee.LastName},
                { "@Email", employee.Email},
                { "@PositionID", employee.PositionID }
            };

            _databaseHelper.ExecuteUpdate(query, parameters);
        }
        public void DeleteEmployee(string employeeID) { }
    }
}
