using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turkish_Talk.Models;
using Turkish_Talk.Services;

namespace Turkish_Talk.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public PersonalCabinetViewModel Cabinet { get; set; }
        public IndexModel(ILogger<IndexModel> logger, UserService userService)
        {
            _logger = logger;
            Cabinet = userService.GetCabinetViewModel().Result;
        }

        public void OnGet()
        {

        }
    }
}
