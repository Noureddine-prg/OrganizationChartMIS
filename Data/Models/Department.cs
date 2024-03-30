namespace OrganizationChartMIS.Data.Models
{
    public class Department
    {
        public string Doid { get; set; }
        public string Name { get; set; }
        public string ReportsTo { get; set; }

        public Department() { }
        public Department(string doid, string name, string reportsTo = null)
        {
            Doid = doid;
            Name = name;
            ReportsTo = reportsTo;
        }
    }
}
