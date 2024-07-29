using OrganizationChartMIS.Data.Context;
using System.Data;
using DepartmentObject = OrganizationChartMIS.Data.Models.Department;


namespace OrganizationChartMIS.Data.Repositories.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public DepartmentRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        public DepartmentObject GetDepartment(string doid)
        {
            try
            {
                Models.Department department = null;
                string query = "SELECT doid, name, reportsTo FROM department WHERE doid = @Doid";
                var parameters = new Dictionary<string, object> { { "@Doid", doid } };

                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    department = new Models.Department
                    {
                        Doid = row["doid"].ToString(),
                        Name = row["name"].ToString(),
                        ReportsTo = row.IsNull("reportsTo") ? null : row["reportsTo"].ToString()
                    };
                }

                return department;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching department: {ex.Message}");
                return null;
            }
        }

        public void AddDepartment(DepartmentObject department)
        {
            try
            {
                string query = "INSERT INTO department (doid, name, reportsTo) VALUES (@Doid, @Name, @ReportsTo)";
                var parameters = new Dictionary<string, object>
                {
                    { "@Doid", department.Doid },
                    { "@Name", department.Name },
                    { "@ReportsTo", department.ReportsTo ?? (object)DBNull.Value }
                };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Department added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add department: {ex.Message}");
            }
        }

        public void UpdateDepartment(DepartmentObject department)
        {
            try
            {
                string query = "UPDATE department SET name = @Name, reportsTo = @ReportsTo WHERE doid = @Doid";
                var parameters = new Dictionary<string, object>
                {
                    { "@Doid", department.Doid },
                    { "@Name", department.Name },
                    { "@ReportsTo", department.ReportsTo ?? (object)DBNull.Value }
                };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Department updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update department: {ex.Message}");
            }
        }

        public void DeleteDepartment(string doid)
        {
            try
            {
                string query = "DELETE FROM department WHERE doid = @Doid";
                var parameters = new Dictionary<string, object> { { "@Doid", doid } };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete department: {ex.Message}");
            }
        }

        public List<DepartmentObject> GetAllDepartments()
        {
            var departments = new List<Models.Department>();
            string query = "SELECT doid, name, reportsTo FROM department ORDER BY name";

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    departments.Add(new Models.Department
                    {
                        Doid = row["doid"].ToString(),
                        Name = row["name"].ToString(),
                        ReportsTo = row.IsNull("reportsTo") ? null : row["reportsTo"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching all departments: {ex.Message}");
            }

            return departments;
        }

        public bool CheckDoidExists(string doid)
        {
            string query = "SELECT COUNT(*) FROM department WHERE doid = @Doid";
            var parameters = new Dictionary<string, object> { { "@Doid", doid } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }

            return false;
        }
    }
}
