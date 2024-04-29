
using EmployeeObject = OrganizationChartMIS.Data.Models.Employee;
using OrganizationChartMIS.Data.Context;
using System.Data;

namespace OrganizationChartMIS.Data.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public EmployeeRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        public EmployeeObject GetEmployee(string emid)
        {
            EmployeeObject employee = null;
            string query = "SELECT emid, email, name, reportsTo, status, positionId FROM employee WHERE emid = @Emid";
            var parameters = new Dictionary<string, object> { { "@Emid", emid } };

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    employee = new EmployeeObject
                    {
                        Emid = row["emid"].ToString(),
                        Email = row["email"].ToString(),
                        Name = row["name"].ToString(),
                        ReportsTo = row.IsNull("reportsTo") ? null : row["reportsTo"].ToString(),
                        Status = row["status"].ToString(),
                        PositionId = row["positionId"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetEmployee - Exception: {ex.Message}");
            }

            return employee;
        }

        public void AddEmployee(EmployeeObject employee)
        {
            string query = @"
            INSERT INTO employee (emid, email, name, reportsTo, status, positionId) 
            VALUES (@Emid, @Email, @Name, @ReportsTo, @Status, @PositionId)";

            var parameters = new Dictionary<string, object>
            {
                { "@Emid", employee.Emid },
                { "@Email", employee.Email },
                { "@Name", employee.Name },
                { "@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value },
                { "@Status", employee.Status },
                { "@PositionId", employee.PositionId }
            };

            try
            {
                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine($"Employee Added: {employee.Emid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddEmployee - Exception: {ex.Message}");
            }
        }


        public void UpdateEmployee(EmployeeObject employee)
        {
            string query = @"
            UPDATE employee 
            SET 
            email = @Email, 
            name = @Name, 
            reportsTo = @ReportsTo, 
            status = @Status, 
            positionId = @PositionId 
            WHERE emid = @Emid";

            var parameters = new Dictionary<string, object>
            {
                { "@Emid", employee.Emid },
                { "@Email", employee.Email },
                { "@Name", employee.Name },
                { "@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value },
                { "@Status", employee.Status },
                { "@PositionId", employee.PositionId }
            };

            try
            {
                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine($"Employee Updated: {employee.Emid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateEmployee - Exception: {ex.Message}");
            }
        }

        public void DeleteEmployee(string emid)
        {
            string query = @"
            UPDATE employee 
            SET status = 'Inactive' 
            WHERE emid = @Emid";

            var parameters = new Dictionary<string, object> { { "@Emid", emid } };

            try
            {
                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine($"Employee Marked as Inactive: {emid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteEmployee - Exception: {ex.Message}");
            }
        }

        public List<EmployeeObject> GetAllSupervisorsByDepartment(string selectedDepartment)
        {
            List<EmployeeObject> supervisors = new List<EmployeeObject>();
            string query = @"
            SELECT e.emid, e.email, e.name, e.reportsTo, e.status, e.positionId 
            FROM employee e
            JOIN position p ON e.positionId = p.poid
            WHERE p.departmentId = @Department
            AND p.level <= (SELECT MIN(level) + 1 FROM position WHERE departmentId = @Department)
            ORDER BY e.name";
            var parameters = new Dictionary<string, object> { { "@Department", selectedDepartment } };

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);
                foreach (DataRow row in dataTable.Rows)
                {
                    supervisors.Add(new EmployeeObject
                    {
                        Emid = row["emid"].ToString(),
                        Email = row["email"].ToString(),
                        Name = row["name"].ToString(),
                        ReportsTo = row.IsNull("reportsTo") ? null : row["reportsTo"].ToString(),
                        Status = row["status"].ToString(),
                        PositionId = row["positionId"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllSupervisorsByDepartment - Exception: {ex.Message}");
            }

            return supervisors;
        }

        public string GetSupervisorIDByNameAndEmail(string supervisorName, string email)
        {
            try
            {
                string query = @"
                SELECT emid 
                FROM employee 
                WHERE name = @SupervisorName AND email = @SupervisorEmail";

                var parameters = new Dictionary<string, object>
                {
                    { "@SupervisorName", supervisorName },
                    { "@SupervisorEmail", email }
                };

                object result = _databaseHelper.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSupervisorIDByNameAndEmail - Exception: {ex.Message}");
                return null;
            }
        }

        public List<EmployeeObject> GetAllEmployees()
        {
            List<EmployeeObject> employees = new List<EmployeeObject>();
            string query = @"
            SELECT emid, email, name, reportsTo, status, positionId 
            FROM employee";

            DataTable dataTable = _databaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                employees.Add(new EmployeeObject
                {
                    Emid = row["emid"].ToString(),
                    Email = row["email"].ToString(),
                    Name = row["name"].ToString(),
                    ReportsTo = row.IsNull("reportsTo") ? null : row["reportsTo"].ToString(),
                    Status = row["status"].ToString(),
                    PositionId = row["positionId"].ToString()
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


        public string GetDepartmentIdByEmployeePosition(string positionId) 
        {
            string query = "SELECT departmentId FROM position WHERE poid = @PositionId";
            var parameters = new Dictionary<string, object> { { "@PositionId", positionId } };
            string departmentId = null;

            try 
            {
                DataTable result = _databaseHelper.ExecuteQuery(query, parameters);
                if (result.Rows.Count > 0) 
                {
                    departmentId = result.Rows[0]["departmentId"].ToString();
                }
            }
            catch (Exception ex) 
            {
                departmentId="";
                Console.WriteLine($"GetDepartmentByPositionId - Exception: {ex.Message}");
            }
            
            return departmentId;
        }
    }

}
