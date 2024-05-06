using System.Collections.Generic;

namespace OrganizationChartMIS.Data.Models
{
    public class Position
    {
        public string Poid { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public int Level { get; set; }
        public string DepartmentName { get; set; }
        public string TeamName { get; set; }
        public string ParentPositionName { get; set; } 

        public Position() { }

        public Position(string poid, string name, string departmentId, int level)
        {
            Poid = poid;
            Name = name;
            DepartmentId = departmentId;
            Level = level;
        }
    }
}

