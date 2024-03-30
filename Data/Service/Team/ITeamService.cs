using TeamObject = OrganizationChartMIS.Data.Models.Team;

namespace OrganizationChartMIS.Data.Service.Team
{
    public interface ITeamService
    {
        TeamObject CreateAndSaveTeam(string name, string departmentId);
        TeamObject GetTeam(string teamId);
        TeamObject UpdateTeam(string teamId, string name, string departmentId);
        bool DeleteTeam(string teamId);
        List<TeamObject> GetAllTeams();
        string GenerateUniqueTeamId();
    }
}
