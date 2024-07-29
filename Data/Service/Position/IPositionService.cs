using PositionObject = OrganizationChartMIS.Data.Models.Position;

namespace OrganizationChartMIS.Data.Service.Position
{
    public interface IPositionService
    {
        PositionObject CreateAndSavePosition(string name, int level, string departmentId);
        PositionObject GetPosition(string poid);
        PositionObject UpdatePosition(string poid, string name, int level, string departmentId);
        bool DeletePosition(string poid);
        
        List<PositionObject> GetAllPositions();
        List<PositionObject> GetPositionsByDepartment(string department);
    }
}
