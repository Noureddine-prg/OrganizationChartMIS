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
            { "@Email", employee.Email },
            { "@Name", employee.Name },
            { "@ParentId", (object)employee.ParentId ?? DBNull.Value },
            { "@Status", employee.Status },
            { "@PositionId", employee.PositionId }
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

        public List<Employee> GetAllSupervisorsByDepartment(string selectedDepartment)
        {
            string query = @"
            SELECT e.emid, e.email, e.name, e.parentId, e.status, e.positionId 
            FROM employee e
            JOIN position p ON e.positionId = p.poid
            WHERE p.Department = @Department 
            AND p.hierarchyLevel IN ('Manager', 'DepartmentLead', 'AssistantManager', 'Supervisor')
            ORDER BY e.name;";

            var parameters = new Dictionary<string, object> { { "@Department", selectedDepartment } };
            var supervisorsByDepartment = new List<Employee>();

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    supervisorsByDepartment.Add(new Employee
                    {
                        Emid = row["emid"].ToString()!,
                        Email = row["email"].ToString()!,
                        Name = row["name"].ToString()!,
                        ParentId = row.IsNull("parentId") ? null : row["parentId"].ToString(),
                        Status = row["status"].ToString()!,
                        PositionId = row["positionId"].ToString()!
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return supervisorsByDepartment;
        }

        public string GetSupervisorIDByNameAndEmail(string supervisorName, string email)
        {
            try
            {
                string query = "SELECT emid FROM employee WHERE name = @SupervisorName AND email = @SupervisorEmail";
                var parameters = new Dictionary<string, object>
                {
                    { "@SupervisorName", supervisorName },
                    { "@SupervisorEmail", email }
                };

                object result = _databaseHelper.ExecuteScalar(query, parameters);

                Console.WriteLine($"Received supervisor id: {result}");

                return result == DBNull.Value ? null : result?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSupervisorIDByNameAndEmail - Exception: {ex.Message}");
                return null; 
            }
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

        public bool CheckEmidExists(string emid)
        {
            string query = "SELECT COUNT(*) FROM employee WHERE Emid = @Emid";
            var parameters = new Dictionary<string, object> { { "@Emid", emid } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }
            
            return false;
        }

    }

}
