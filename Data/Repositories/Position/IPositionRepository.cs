using PositionObject = OrganizationChartMIS.Data.Models.Position;


namespace OrganizationChartMIS.Data.Repositories.Position
{
    public interface IPositionRepository
    {
        List<PositionObject> GetAllPositions();
        PositionObject GetPosition(string positionId);
        void AddPosition(PositionObject position);
        void UpdatePosition(PositionObject position);
        void DeletePosition(string positionId);
    }
}
