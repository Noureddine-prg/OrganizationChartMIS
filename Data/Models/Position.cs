using System.Collections.Generic;

namespace OrganizationChartMIS.Data.Models
{
    public class Position
    {
        public string PositionID { get; set; }
        public string CurrentEmployee { get; set; } = "Vacant";
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ParentPositionID { get; set; }
        public Position ParentPosition { get; set; }
        public ICollection<Position> ChildPositions { get; set; }

        public Position()
        {
            ChildPositions = new HashSet<Position>();
        }
    }
}

