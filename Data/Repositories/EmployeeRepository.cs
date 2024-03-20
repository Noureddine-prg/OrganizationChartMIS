using OrganizationChartMIS.Data.Models;
using OrganizationChartMIS.Data.Context;
using System.Data;

namespace OrganizationChartMIS.Data.Repositories
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
            string query = "SELECT emid, email, name, parentId, status, positionId FROM employee";


            DataTable dataTable = _databaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                employees.Add(new Employee
                {
                    Emid = row["emid"].ToString()!,
                    Email = row["email"].ToString()!,
                    Name = row["name"].ToString()!,
                    ParentId = row.IsNull("parentId") ? null : row["parentId"].ToString(),
                    Status = row["status"].ToString()!,
                    PositionId = row["positionId"].ToString()!
                });
            }

            return employees;
        }

        public Employee GetEmployee(string emid)
        {
            Employee employee = null;
            string query = "SELECT emid, email, name, parentId, status, positionId FROM Employees WHERE emid = @Emid";


            var parameters = new Dictionary<string, object> { { "@Emid", emid } };

            DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                employee = new Employee
                {
                    Emid = row["emid"].ToString()!,
                    Email = row["email"].ToString()!,
                    Name = row["name"].ToString()!,
                    ParentId = row.IsNull("parentId") ? null : row["parentId"].ToString()!,
                    Status = row["status"].ToString()!,
                    PositionId = row["positionId"].ToString()!
                };
            }

            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            string query = @"
            INSERT INTO employee (emid, email, name, parentId, status, positionId) 
            VALUES (@Emid, @Email, @Name, @ParentId, @Status, @PositionId)";

            var parameters = new Dictionary<string, object>
            {
                { "@Emid", employee.Emid },
                { "@Email", employee.Email},
                { "@Name", employee.Name},
                { "@ParentId", (object)employee.ParentId ?? DBNull.Value },
                { "@Status", employee.Status },
                { "@PositionId", employee.PositionId}
            };

            _databaseHelper.ExecuteUpdate(query, parameters);
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = @"
                UPDATE Employees 
                SET 
                email = @Email, 
                name = @Name, 
                parentId = @ParentId, 
                status = @Status, 
                positionId = @PositionId
                WHERE emid = @Emid";

            var parameters = new Dictionary<string, object>
            {
                { "@Emid", employee.Emid },
                { "@Email", employee.Email },
                { "@Name", employee.Name },
                { "@ParentId", (object)employee.ParentId ?? DBNull.Value }, 
                { "@Status", employee.Status },
                { "@PositionId", employee.PositionId }
            };

            _databaseHelper.ExecuteUpdate(query, parameters);
        }

        public void DeleteEmployee(string emid)
        {
            string query = @"
                UPDATE employee
                SET status = 'Inactive'
                WHERE emid = @Emid
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@Emid", emid }
            };

            _databaseHelper.ExecuteUpdate(query, parameters);
        }
    }
}
