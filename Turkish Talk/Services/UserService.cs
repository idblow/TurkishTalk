using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Models;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

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

        public void StoreValueInSession(string key, string value)
        {
            _httpContext.Session.SetString(key, value);
        }

        public string? GetValueFromSession(string key)
        {
            return _httpContext.Session.GetString(key);
        }

        private int? GetUserIdFromSession()
        {
            return _httpContext.Session.GetInt32("userid");
        }


        public async Task<PersonalCabinetViewModel?> GetCabinetViewModel()
        {
            var userid = GetUserIdFromSession();

            if(userid == null)
            {
                return null;
            }

            var user = await _applicationDBContext.Set<User>().Where(x => x.Id == userid)
                .Include(x => x.ProgresWrite)
                .Include(x => x.ProgressAlfabet)
                .Include(x => x.ProgresRead)
                .Include(x => x.ProgresGrammar).FirstAsync();

            var view = new PersonalCabinetViewModel();

            view.FullName = user.FullName;
            view.ScoreGrammar = await CountCorrectAnswerGrammarAsync(user.ProgresGrammar);
            view.ScoreWrite = await CountCorrectAnswerWriteAsync(user.ProgresWrite);
            view.ScoreAlphabet = await CountCorrectAnswerAlfabetAsync(user.ProgressAlfabet);
            view.ScoreRead = await CountCorrectAnswerReadAsync(user.ProgresRead);
            view.TotalScore = TotalCountCorrectAnswer(view.ScoreGrammar, view.ScoreWrite, view.ScoreAlphabet,view.ScoreRead);
            view.Id = user.Id;

            return view;
        }

        public async Task<int> CountCorrectAnswerReadAsync(List<ProgresRead> progres)
        {
            var countreadtask = await _applicationDBContext.Set<ReadTask>().CountAsync();
            var fullprogress = countreadtask * 100;

            if (fullprogress == 0)
            {
                fullprogress = 1;
            }

            var realprogress = progres.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * 100) / fullprogress;
            return totalprogress;

        }

        public async Task<int> CountCorrectAnswerWriteAsync(List<ProgresWrite> progresWrite)
        {
            var countwritetask = await _applicationDBContext.Set<WriteTask>().CountAsync();
            var fullprogress = countwritetask * 100;

            if (fullprogress == 0)
            {
                fullprogress = 1;
            }

            var realprogress = progresWrite.Select(x => x.Score).Sum();
            var totalprogress = (realprogress * 100) / fullprogress;
            return totalprogress;
        }

        public async Task<int> CountCorrectAnswerAlfabetAsync(List<ProgressAlfabet> progres)
        {
            var countalphabettask = await _applicationDBContext.Set<AlfabetTask>().CountAsync();
            var fullprogress = countalphabettask * 100;

            if (fullprogress == 0)
            {
                fullprogress = 1;
            }

            var realprogress = progres.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * 100) / fullprogress;
            return totalprogress;
        }

        public async Task<int> CountCorrectAnswerGrammarAsync(List<ProgresGrammar> progresGrammars)
        {
            var countgrammar = await _applicationDBContext.Set<GrammarTask>().CountAsync();
            
            var fullprogress = countgrammar * 100;

            if(fullprogress == 0)
            {
                fullprogress = 1;
            }

            var realprogress = progresGrammars.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * 100)/fullprogress;
            return totalprogress;
        }

        public int TotalCountCorrectAnswer (int progressread, int progresswrite, int progressalfabet, int progressgrammar)
        {
            return (progressread + progresswrite + progressgrammar + progressalfabet) / 4;
        }

    }
}
