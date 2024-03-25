namespace OrganizationChartMIS.Data.Models
{

    public class Employee
    {

        public string Emid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Status { get; set; }
        public string PositionId { get; set; }

        public string PositionName { get; set; }
        public string SupervisorName { get; set; }

        public Employee() { }

        public Employee(string Emid, string Email, string Name, string ParentId, string Status, string PositionId) {
            this.Emid = Emid;
            this.Email = Email;
            this.Name = Name;
            this.ParentId = ParentId ;
            this.Status = Status;
            this.PositionId = PositionId;
        }




    }

}
