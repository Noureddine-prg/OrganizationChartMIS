using OrganizationChartMIS.Data.Context;
using OrganizationChartMIS.Data.Models;
using System.Data;

namespace OrganizationChartMIS.Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {

        private readonly DatabaseHelper _databaseHelper;

        public PositionRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        public List<Position> GetAllPositions()
        {

            var positions = new List<Position>();
            string query = "SELECT poid, name, department, hierarchyLevel FROM position";

            try
            {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    positions.Add(new Position
                    {
                        Poid = row["poid"].ToString()!,
                        Name = row["name"].ToString()!,
                        Department = row["department"].ToString()!,
                        HierarchyLevel = row["hierarchyLevel"].ToString()!
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return positions;
        }

        public Position GetPosition(string poid)
        {

            Position position = null;
            string query = "SELECT poid, name, department, hierarchyLevel FROM position WHERE poid = @Poid";
            var parameters = new Dictionary<string, object> { { "@Poid", poid } };
 

            DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                position = new Position
                {
                    Poid = row["poid"].ToString()!,
                    Name = row["name"].ToString()!,
                    Department = row["department"].ToString()!,
                    HierarchyLevel = row["hierarchyLevel"].ToString()!
                };
            }

            return position!;

        }

        public void AddPosition(Position position)
        {

            string query = "INSERT INTO position (poid, name, department, hierarchyLevel) VALUES (@Poid, @Name, @Department, @HierarchyLevel)";
            var parameters = new Dictionary<string, object>
            {
                { "@Poid", position.Poid},
                { "@Name", position.Name},
                { "@Department", position.Department},
                { "@HierarchyLevel", position.HierarchyLevel}
            };

            try
            {
                _databaseHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

        }

        public void UpdatePosition(Position position)
        {

            string query = "UPDATE position SET name = @Name, department = @Department, hierarchyLevel = @HierarchyLevel WHERE poid = @Poid";
            var parameters = new Dictionary<string, object>
            {
                { "@Poid", position.Poid },
                { "@Name", position.Name },
                { "@Department", position.Department },
                { "@HierarchyLevel", position.HierarchyLevel }
            };

            try
            {
                _databaseHelper.ExecuteUpdate(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

        }

        public void DeletePosition(string poid)
        {

            string query = "DELETE FROM position WHERE PositionID = @Poid";

            var parameters = new Dictionary<string, object>
            {
                { "@Poid", poid}
            };

            try
            {
                _databaseHelper.ExecuteUpdate(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

        }

    }
}
