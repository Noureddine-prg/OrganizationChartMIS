namespace OrganizationChartMIS.Data.Models
{
    public class Employee
    {
        public string Emid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ReportsTo { get; set; }
        public string Status { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string SupervisorName { get; set; }

        public Employee() { }

        public Employee(string emid, string email, string name, string status, string positionId)
        {
            Emid = emid;
            Email = email;
            Name = name;
            Status = status;
            ReportsTo = null;
            PositionId = positionId; 
        }
    }
}
