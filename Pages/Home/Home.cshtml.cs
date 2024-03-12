using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrganizationChartMIS.Pages.Home
{
    public class HomeModel : PageModel
    {
        public string? Name { get; set; }

        public void OnGet()
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            Console.WriteLine(Name);

            //employee leader manager - levels  
            //figure out how to call create addposition function
            //who are the employees reporting to
            //linked list 
            //org tree 
            //each node points to who they report 
            Console.WriteLine("Test Connection");


        }

    }
}
