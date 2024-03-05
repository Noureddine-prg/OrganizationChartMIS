using OrganizationChartMIS.Data.DatabaseHelper;
using OrganizationChartMIS.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OrganizationChartMIS.Repositories
{
    public class PositionRepository : IPositionRepository
    {

        private readonly DatabaseHelper _databaseHelper;

        public PositionRepository(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        public List<Position> GetAllPositions() {
            
            var positions = new List<Position>();
            string query = "SELECT PositionID, Title, Description, ParentPositionID, CurrentEmployee FROM Positions";

            try {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    positions.Add(new Position
                    {
                        PositionID = row["PositionID"].ToString()!,
                        Title = row["Title"].ToString()!,
                        Description = row.IsNull("Description") ? null : row["Description"].ToString(),
                        ParentPositionID = row.IsNull("ParentPositionID") ? null : row["ParentPositionID"].ToString(),
                        CurrentEmployee = row.IsNull("CurrentEmployee") ? "Vacant" : row["CurrentEmployee"].ToString()                
                    }); 
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return positions;
        }

        public Position GetPosition(string positionId) {
            
            Position position = null;

            string query = @"
                SELECT PositionID, Title, ParentPositionID, CurrentEmployee 
                FROM Positions 
                WHERE PositionID = @PositionID";

            var parameters = new Dictionary<string, object>
            {
                { "@PositionID", positionId}
            };

            DataTable dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0) 
            {
                DataRow row  = dataTable.Rows[0];
                position = new Position
                {
                    PositionID = row["PositionID"].ToString()!,
                    Title = row["Title"].ToString()!,
                    Description = row.IsNull("Description") ? null : row["Description"].ToString(),
                    ParentPositionID = row.IsNull("ParentPositionID") ? null : row["ParentPositionID"].ToString(),
                    CurrentEmployee = row.IsNull("CurrentEmployee") ? "Vacant" : row["CurrentEmployee"].ToString()
                };
            }

            return position;
                 
        }
        
        public void AddPosition(Position position) {
            
            string query = @"
                INSERT INTO Positions(PositionId,Title,Description,ParentPositionID)
                VALUES(@PositionID, @Title, @Description, @ParentPositionID)
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@PositionID", position.PositionID},
                { "@Title", position.Title},
                { "@Description", position.Description},
                { "@ParentPositionID", (object)position.ParentPositionID ?? DBNull.Value}
            };

            try 
            {
                _databaseHelper.ExecuteQuery(query, parameters);
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message, ex); 
            }
            
        }

        public void UpdatePosition(Position position) {
            
            string query = 
            @"UPDATE Positions SET 
            Title = @Title,
            Description = @Description,
            ParentPositionID = @ParentPositionID
            WHERE PositionID = @PositionID";

            var parameters = new Dictionary<string, object>
            {
                { "@PositionID", position.PositionID },
                { "@Title", position.Title},
                { "@Description", position.Description },
                { "@ParentPositionID", (object)position.ParentPositionID ?? DBNull.Value}
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

        public void DeletePosition(string positionId) {
            
            string query = "DELETE FROM Positions WHERE PositionID = @PositionID";

            var parameters = new Dictionary<string, object>
            {
                { "@PositionID", positionId}
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
