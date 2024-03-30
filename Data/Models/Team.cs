namespace OrganizationChartMIS.Data.Models
{
    public class Team
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string DepartmentId { get; set; }

        public Team() { }
         
        public Team(string teamId, string teamName, string departmentId)
        {
            TeamId = teamId;
            TeamName = teamName;
            DepartmentId = departmentId;
        }
    }
}
