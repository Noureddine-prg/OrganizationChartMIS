namespace OrganizationChartMIS.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public List<PositionAttribute> Attributes { get; set; } = new List<PositionAttribute>();
    }

    public class PositionAttribute { 
        public int PositionAttributeID { get; set; }
        public int PositionID { get; set; }
        public string Key { get; set; } 
        public string Value { get; set; }
        public Position Position { get; set; }
    }
}



