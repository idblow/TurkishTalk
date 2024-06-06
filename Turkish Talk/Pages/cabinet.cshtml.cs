using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Models;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{

    public class cabinetModel : PageModel 
    {
        private readonly UserService _userService;
        private readonly ApplicationDBContext _applicationDBContext;

        public cabinetModel(UserService userService, ApplicationDBContext applicationDBContext) 
        {
            PersonalCabinetViewModel = userService.GetCabinetViewModel().Result;
            _userService = userService;
            _applicationDBContext = applicationDBContext;
        }

        public PersonalCabinetViewModel PersonalCabinetViewModel { get; set; }

        public async Task OnPostImageUpdateAsync(IFormFile image)
        {
            var userId = _userService.GetUserIdFromSession();

            if(userId.HasValue == false)
            {
                return;
            }

            var user = await _applicationDBContext.Set<User>().FirstAsync(x => x.Id == userId.Value);
            user.Image = image.OpenReadStream().ToArray();
            user.ImageContentType = image.ContentType;
            _applicationDBContext.Update(user);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostUserNameUpdateAsync(string name)
        {
            var userId = _userService.GetUserIdFromSession();

            if (userId.HasValue == false)
            {
                return;
            }

            var user = await _applicationDBContext.Set<User>().FirstAsync(x => x.Id == userId.Value);
            user.FullName = name;
            _applicationDBContext.Update(user);
            await _applicationDBContext.SaveChangesAsync();
        }
        public void OnGet()
        {
           
        
        }
    }
}
