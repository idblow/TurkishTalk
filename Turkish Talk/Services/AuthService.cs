using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
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

            if(user!=null && VerifyPassword(password, user.Salt, user.HeshedPassword))
            {
                WriteUserIdToSession(user.Id);
                return true;
            }

            return false;
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

            var salt = GenerateSalt();

           var userEntry = _applicationDB.Set<User>().Add(new User
            {
                Login = login,
                FullName = fullname,
                HeshedPassword = HashPassword(password, salt),
                Salt = salt,
               // Image = Array.Empty<byte>()
            });

            await _applicationDB.SaveChangesAsync();

            WriteUserIdToSession(userEntry.Entity.Id);
        }

        private void WriteUserIdToSession(int  userId)
        {
            _httpContext.Session.SetInt32("userid", userId);
        }

        private static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }
        private static string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private bool VerifyPassword(string enteredPassword, string salt, string storedHash)
        {
            return HashPassword(enteredPassword, salt) == storedHash;
        }
    }
}
