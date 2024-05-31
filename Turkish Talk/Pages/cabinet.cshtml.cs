using Microsoft.AspNetCore.Mvc.RazorPages;
using Turkish_Talk.Models;
using Turkish_Talk.Services;

namespace Turkish_Talk.Pages
{

    public class cabinetModel : PageModel 
    {
        public cabinetModel(UserService userService) 
        {
           PersonalCabinetViewModel = userService.GetCabinetViewModel().Result;
        }

        public PersonalCabinetViewModel PersonalCabinetViewModel { get; set; }
        public void OnGet()
        {
           
        
        }
    }
}
