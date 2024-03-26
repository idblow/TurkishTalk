using Microsoft.EntityFrameworkCore;
using System.Data;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Services
{
    public class AuthService
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly HttpContext _httpContext;

        public AuthService(ApplicationDBContext applicationDB, IHttpContextAccessor httpContext)
        {
            _applicationDB = applicationDB;

            _httpContext = httpContext.HttpContext!;
        }

        public async Task<bool> AuthUserAsync(string login, string password)
        {
            var user = await _applicationDB.Set<User>().Where(x => x.Login == login).FirstOrDefaultAsync();

            if (user == null || password != user.HeshedPassword)
            {
                return false;
            }

            WriteUserIdToSession(user.Id);

            return true;
        }
        public int? GetUserId()
        {
            return _httpContext.Session.GetInt32("userid");
        }


        public async Task RegistationUserAsync(string login, string password, string dublicatepassword, string fullname)
        {
            if (password != dublicatepassword)
            {
                throw new Exception("Введенные пароли не совпадают");
            }

            var user = await _applicationDB.Set<User>().Where(x => x.Login == login).FirstOrDefaultAsync();

            if (user != null)
            {
                throw new Exception("Пользователь уже существует");
            }

           var userEntry = _applicationDB.Set<User>().Add(new User
            {
                Login = login,
                FullName = fullname,
                HeshedPassword = password,
                Salt = string.Empty,
               // Image = Array.Empty<byte>()
            });

            await _applicationDB.SaveChangesAsync();

            WriteUserIdToSession(userEntry.Entity.Id);
        }

        private void WriteUserIdToSession(int  userId)
        {
            _httpContext.Session.SetInt32("userid", userId);
        }
    }
}
