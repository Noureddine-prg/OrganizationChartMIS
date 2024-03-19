using Kendo.Mvc.UI;

namespace OrganizationChartMIS.Data.Models
{
    public class OrgChartNode
    {
        //here we want to combine the data we get from position and employee ad create an org chart node based off of our needs 
 
        public string ID { get; set; }
        public string CurrentEmployee { get; set; }
        public string Title { get; set; }
        public string? ParentPositionID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


    }
}
