using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Repositories
{
    public interface IPositionRepository
    {
        List<Position> GetAllPositions();
        Position GetPosition(string positionId);
        void AddPosition(Position position);
        void UpdatePosition(Position position);
        void DeletePosition(string positionId); 
    }
}
