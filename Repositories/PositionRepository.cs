using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        public PositionRepository() { }
        public void GetPositions() { }
        public void GetPosition(string positionId) { }
        public void AddPosition(Position position) { }
        public void UpdatePosition(Position position) { }
        public void DeletePosition(string positionId) { }
    }
}
