using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turkish_Talk.Models;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;

namespace Turkish_Talk.Pages
{
    public class LoginModel : PageModel
    {
        public PersonalCabinetViewModel _PersonalCabinetViewModel { get; }

        private readonly AuthService _authService;
       
        public LoginModel(AuthService authService, UserService userService)
        {
            _PersonalCabinetViewModel = userService.GetCabinetViewModel().Result;
            _authService = authService;
        }

        public async Task<IActionResult> OnPostLoginAsync(string login, string password)
        {
            var isAuthorized = false;

            try
            {
                isAuthorized = await _authService.AuthUserAsync(login, password);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if(!isAuthorized)
            {
                return Unauthorized();
            }

            return RedirectToPage("cabinet");
        }

        public async Task<IActionResult> OnPostRegistrationAsync(string fullname, string login, string password, string doublepassword)
        {
            try
            {
                await _authService.RegistationUserAsync(login, password, doublepassword, fullname);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToPage("cabinet");
        }
    }
}
