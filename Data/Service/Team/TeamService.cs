using OrganizationChartMIS.Data.Repositories.Team;
using TeamObject = OrganizationChartMIS.Data.Models.Team;

namespace OrganizationChartMIS.Data.Service.Team
{
    public class TeamService : ITeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }

        public TeamObject CreateAndSaveTeam(string name, string departmentId)
        {
            try
            {
                string teamId = GenerateUniqueTeamId();

                TeamObject newTeam = new TeamObject
                {
                    TeamId = teamId,
                    TeamName = name,
                    DepartmentId = departmentId
                };

                _teamRepository.AddTeam(newTeam);
                Console.WriteLine($"CreateAndSaveTeam - Team Added: {newTeam.TeamName}");

                return newTeam;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateAndSaveTeam - Exception: {ex.Message}");
                return null;
            }
        }

        public TeamObject GetTeam(string teamId)
        {
            try
            {
                return _teamRepository.GetTeam(teamId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTeam - Exception: {ex.Message}");
                return null;
            }
        }

        public TeamObject UpdateTeam(string teamId, string name, string departmentId)
        {
            try
            {
                var teamToUpdate = _teamRepository.GetTeam(teamId);

                if (teamToUpdate != null)
                {
                    teamToUpdate.TeamName = name;
                    teamToUpdate.DepartmentId = departmentId;
                    _teamRepository.UpdateTeam(teamToUpdate);
                    Console.WriteLine($"UpdateTeam - Team Updated: {teamToUpdate.TeamName}");

                    return teamToUpdate;
                }
                else
                {
                    Console.WriteLine("UpdateTeam - Team not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateTeam - Exception: {ex.Message}");
                return null;
            }
        }

        public bool DeleteTeam(string teamId)
        {
            try
            {
                _teamRepository.DeleteTeam(teamId);
                Console.WriteLine($"DeleteTeam - Team Deleted: {teamId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteTeam - Exception: {ex.Message}");
                return false;
            }
        }

        public List<TeamObject> GetAllTeams()
        {
            try
            {
                return _teamRepository.GetAllTeams();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllTeams - Exception: {ex.Message}");
                return new List<TeamObject>();
            }
        }

        public string GenerateUniqueTeamId()
        {
            bool exists;
            string teamId;

            do
            {
                var randomNumber = new Random().Next(100000, 999999);
                teamId = $"T{randomNumber}";

                exists = _teamRepository.CheckTeamIdExists(teamId);
            }
            while (exists);

            return teamId;
        }
    }
}

