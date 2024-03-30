using TeamObject = OrganizationChartMIS.Data.Models.Team;

namespace OrganizationChartMIS.Data.Repositories.Team
{
    public interface ITeamRepository
    {
        TeamObject GetTeam(string teamId);
        void AddTeam(TeamObject team);
        void UpdateTeam(TeamObject team);
        void DeleteTeam(string teamId);
        List<TeamObject> GetAllTeams();
        List<TeamObject> GetTeamsByDepartment(string departmentId);
    }
}
