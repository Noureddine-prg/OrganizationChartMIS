using OrganizationChartMIS.Data.Models;

namespace OrganizationChartMIS.Repositories
{
    public interface IPositionRepository
    {
        /*
        public string PositionID { get; set; } //temp
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ParentPositionID { get; set; }
        public Position ParentPosition { get; set; }
        public ICollection<Position> ChildPositions { get; set; }
        */

        IEnumerable<Position> GetAllPositions();
        void GetPosition(string positionId);
        void AddPosition(Position position);
        void UpdatePosition(Position position);
        void DeletePosition(string positionId); //flag inactive never remove
    }
}
