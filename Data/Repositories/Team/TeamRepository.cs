using OrganizationChartMIS.Data.Context;
using System.Data;
using TeamObject = OrganizationChartMIS.Data.Models.Team;


namespace OrganizationChartMIS.Data.Repositories.Team
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public TeamRepository(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public TeamObject GetTeam(string teamId)
        {
            string query = "SELECT teamid, teamName, departmentId FROM team WHERE teamid = @TeamId";
            var parameters = new Dictionary<string, object> { { "@TeamId", teamId } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                return new TeamObject
                {
                    TeamId = row["teamid"].ToString(),
                    TeamName = row["teamName"].ToString(),
                    DepartmentId = row["departmentId"].ToString()
                };
            }
            return null;
        }

        public void AddTeam(TeamObject team)
        {
            string query = @"INSERT INTO team (teamid, teamName, departmentId) 
                             VALUES (@TeamId, @TeamName, @DepartmentId)";
            var parameters = new Dictionary<string, object>
            {
                { "@TeamId", team.TeamId },
                { "@TeamName", team.TeamName },
                { "@DepartmentId", team.DepartmentId }
            };
            _databaseHelper.ExecuteUpdate(query, parameters);
        }

        public void UpdateTeam(TeamObject team)
        {
            string query = @"UPDATE team SET teamName = @TeamName, departmentId = @DepartmentId WHERE teamid = @TeamId";
            var parameters = new Dictionary<string, object>
            {
                { "@TeamId", team.TeamId },
                { "@TeamName", team.TeamName },
                { "@DepartmentId", team.DepartmentId }
            };
            _databaseHelper.ExecuteUpdate(query, parameters);
        }

        public void DeleteTeam(string teamId)
        {
            string query = "DELETE FROM team WHERE teamid = @TeamId";
            var parameters = new Dictionary<string, object> { { "@TeamId", teamId } };
            _databaseHelper.ExecuteUpdate(query, parameters);
        }

        public List<TeamObject> GetAllTeams()
        {
            var teams = new List<TeamObject>();
            string query = "SELECT teamid, teamName, departmentId FROM team";
            var dataTable = _databaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                teams.Add(new TeamObject
                {
                    TeamId = row["teamid"].ToString(),
                    TeamName = row["teamName"].ToString(),
                    DepartmentId = row["departmentId"].ToString()
                });
            }
            return teams;
        }

        public List<TeamObject> GetTeamsByDepartment(string departmentId)
        {
            var teams = new List<TeamObject>();
            string query = "SELECT teamid, teamName, departmentId FROM team WHERE departmentId = @DepartmentId";
            var parameters = new Dictionary<string, object> { { "@DepartmentId", departmentId } };
            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                teams.Add(new TeamObject    
                {
                    TeamId = row["teamid"].ToString(),
                    TeamName = row["teamName"].ToString(),
                    DepartmentId = row["departmentId"].ToString()
                });
            }
            return teams;
        }

        public bool CheckTeamIdExists(string teamId) 
        {
            string query = "SELECT COUNT(*) FROM team WHERE TeamId = @TeamId";
            var parameters = new Dictionary<string, object> { { "@TeamId", teamId } };

            var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] > 0)
            {
                return true;
            }

            return false;
        }
    }
}
