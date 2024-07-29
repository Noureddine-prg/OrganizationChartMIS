using OrganizationChartMIS.Data.Context;
using PositionObject = OrganizationChartMIS.Data.Models.Position;

using System.Data;

namespace OrganizationChartMIS.Data.Repositories.Position
{
    public class PositionRepository : IPositionRepository
    {

        private readonly DatabaseHelper _databaseHelper;

        public PositionRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        public PositionObject GetPosition(string poid)
        {
            try
            {
                PositionObject position = null;
                string query = @"SELECT poid, name, level, departmentId
                    FROM position 
                    WHERE poid = @Poid";
                var parameters = new Dictionary<string, object> { { "@Poid", poid } };

                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    position = new PositionObject
                    {
                        Poid = row["poid"].ToString(),
                        Name = row["name"].ToString(),
                        Level = Convert.ToInt32(row["level"]),
                        DepartmentId = row["departmentId"].ToString(),
                    };
                }
                return position;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the position: {ex.Message}");
                return null;
            }
        }

        public void AddPosition(PositionObject position)
        {
            try
            {
                string query = @"INSERT INTO position (poid, name, level, departmentId) 
                         VALUES (@Poid, @Name, @Level, @ReportsTo, @DepartmentId)";
                var parameters = new Dictionary<string, object>
                {
                    {"@Poid", position.Poid},
                    {"@Name", position.Name},
                    {"@Level", position.Level},
                    {"@DepartmentId", position.DepartmentId},
                };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Position added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add position: {ex.Message}");
            }
        }

        public void UpdatePosition(PositionObject position)
        {
            try
            {
                string query = @"UPDATE position 
                         SET name = @Name, level = @Level, 
                             departmentId = @DepartmentId
                         WHERE poid = @Poid";
                var parameters = new Dictionary<string, object>
                {
                    {"@Poid", position.Poid},
                    {"@Name", position.Name},
                    {"@Level", position.Level},
                    {"@DepartmentId", position.DepartmentId},
                };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Position updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update position: {ex.Message}");
            }
        }

        public void DeletePosition(string poid)
        {
            try
            {
                string query = "DELETE FROM position WHERE poid = @Poid";
                var parameters = new Dictionary<string, object> { { "@Poid", poid } };

                _databaseHelper.ExecuteUpdate(query, parameters);
                Console.WriteLine("Position deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete position: {ex.Message}");
            }
        }

        public List<PositionObject> GetPositionsByDepartment(string selectedDepartment)
        {
            var positions = new List<PositionObject>();
            string query = @"
            SELECT p.poid, p.name, p.level, p.departmentId
            FROM position p
            JOIN department d ON p.departmentId = d.doid
            WHERE d.name = @DepartmentName AND p.level <> 3
            ORDER BY p.level, p.name";
            
            var parameters = new Dictionary<string, object> { { "@DepartmentName", selectedDepartment } };

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);
                foreach (DataRow row in dataTable.Rows)
                {
                    positions.Add(new PositionObject
                    {
                        Poid = row["poid"].ToString(),
                        Name = row["name"].ToString(),
                        Level = Convert.ToInt32(row["level"]),
                        DepartmentId = row["departmentId"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching positions by department: {ex.Message}");
            }

            return positions;
        }

        public List<PositionObject> GetAllPositions()
        {
            var positions = new List<PositionObject>();
            string query = @"
            SELECT poid, name, level, departmentId
            FROM position";

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {
                    positions.Add(new PositionObject
                    {
                        Poid = row["poid"].ToString(),
                        Name = row["name"].ToString(),
                        Level = Convert.ToInt32(row["level"]),
                        DepartmentId = row["departmentId"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching all positions: {ex.Message}");
            }

            return positions;
        }

        public string GetPositionIdByName(string positionName)
        {
            try
            {
                string query = "SELECT poid FROM position WHERE name = @PositionName";
                var parameters = new Dictionary<string, object> { { "@PositionName", positionName } };

                object result = _databaseHelper.ExecuteScalar(query, parameters);

                return result != null ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting position ID by name: {ex.Message}");
                return null;
            }
        }


        public bool CheckPoidExists(string poid)
        {
            string query = "SELECT COUNT(*) FROM position WHERE Poid = @Poid";
            var parameters = new Dictionary<string, object> { { "@Poid", poid } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }

            return false;
        }

    }
}
