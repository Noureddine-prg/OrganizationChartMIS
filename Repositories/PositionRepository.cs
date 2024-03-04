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

        public IEnumerable<Position> GetAllPositions() {
            
            var positions = new List<Position>();
            string query = "SELECT PositionID, Title, Description, ParentPositionID, CurrentEmployee FROM Positions";

            try {
                DataTable dataTable = _databaseHelper.ExecuteQuery(query);
            
            }
            catch(Exception) { }

            return positions;
        }
        public void GetPosition(string positionId) { }
        public void AddPosition(Position position) { }
        public void UpdatePosition(Position position) { }
        public void DeletePosition(string positionId) { }
    }
}
