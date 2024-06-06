using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Models;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Services
{
    public class UserService
    {
        private ApplicationDBContext _applicationDBContext;
        private int _countgrammar;
        private int _countalphabettask;
        private int _countwritetask;
        private int _countreadtask;
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

        public int? GetUserIdFromSession()
        {
            return _httpContext.Session.GetInt32("userid");
        }


        public async Task<PersonalCabinetViewModel?> GetCabinetViewModel()
        {
            var userid = GetUserIdFromSession();

            if (userid == null)
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
            view.TotalScore = TotalCountCorrectAnswer(user);
            view.Id = user.Id;
            view.IsAdmin = user.Role == UserRole.Admin;

            return view;
        }

        public async Task<int> CountCorrectAnswerReadAsync(List<ProgresRead> progres)
        {
            _countreadtask = await _applicationDBContext.Set<ReadTask>().CountAsync();
            var fullprogress = _countreadtask;

            var realprogress = progres.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * fullprogress) / 100;
            return totalprogress;

        }

        public async Task<int> CountCorrectAnswerWriteAsync(List<ProgresWrite> progresWrite)
        {
            _countwritetask = await _applicationDBContext.Set<WriteTask>().CountAsync();
            var fullprogress = _countwritetask;

            var realprogress = progresWrite.Select(x => x.Score).Sum();
            var totalprogress = (realprogress * fullprogress) / 100;
            return totalprogress;
        }

        public async Task<int> CountCorrectAnswerAlfabetAsync(List<ProgressAlfabet> progres)
        {
            _countalphabettask = await _applicationDBContext.Set<AlfabetTask>().CountAsync();
            var fullprogress = _countalphabettask;

            var realprogress = progres.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * fullprogress) / 100;
            return totalprogress;
        }

        public async Task<int> CountCorrectAnswerGrammarAsync(List<ProgresGrammar> progresGrammars)
        {
            _countgrammar = await _applicationDBContext.Set<GrammarTask>().CountAsync();

            var fullprogress = _countgrammar;

            var realprogress = progresGrammars.Select(x => x.scope).Sum();
            var totalprogress = (realprogress * fullprogress) / 100;
            return totalprogress;
        }

        public int TotalCountCorrectAnswer(User user)
        {
            var userPoints = user.ProgressAlfabet.Select(x => x.scope).Sum()
                + user.ProgresGrammar.Select(x => x.scope).Sum()
                + user.ProgresRead.Select(x => x.scope).Sum()
                + user.ProgresWrite.Select(x => x.Score).Sum();

            var totalPoints = _countalphabettask + _countgrammar + _countreadtask + _countwritetask;

            return (userPoints / totalPoints) * 100;
        }

    }
}
