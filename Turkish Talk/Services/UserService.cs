using Turkish_Talk.Models;
using TurkishTalk.Persistance;

namespace Turkish_Talk.Services
{
    public class UserService
    {
        private ApplicationDBContext _applicationDBContext;
        private readonly HttpContext _httpContext;

        public UserService(ApplicationDBContext applicationDBContext, IHttpContextAccessor httpContext)
        {
            _applicationDBContext = applicationDBContext;

            _httpContext = httpContext.HttpContext!;
        }

        private int? GetUserIdFromSession()
        {
            return _httpContext.Session.GetInt32("userid");
        }


        //public async Task<PersonalCabinetViewModel> GetCabinetViewModel()
        //{

        //}
    }
}
