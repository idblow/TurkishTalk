using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class alfabetModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public alfabetModel(ApplicationDBContext applicationDB, AuthService authService, UserService userService)
        {
            _applicationDB = applicationDB;
            _authService = authService;
            _userService = userService;
            
            var userId = _authService.GetUserId();
            if (userId.HasValue)
            {
                var activeTaskId = _userService.GetValueFromSession("ActiveAlphabetTask");
                TaskTopics =  _applicationDB.Set<AlfabetTask>().Select(x=>x.Name).ToList();
            
               var activeTaskQuery = (IQueryable<AlfabetTask>)_applicationDB.Set<AlfabetTask>();

               if (!string.IsNullOrEmpty(activeTaskId))
               {
                   activeTaskQuery = activeTaskQuery.Where(p => p.Id == int.Parse(activeTaskId));
               }

               ActiveTask = activeTaskQuery.Include(x => x.WordDictionary)
                   .Include(x=>x.ProgressAlfabet.Where(y=>y.User.Id==userId))
                   .First();
                
               _userService.StoreValueInSession("ActiveAlphabetTask", ActiveTask.Id.ToString());
               
                _progressCurrentTask = ActiveTask.ProgressAlfabet.FirstOrDefault();
                var words = ActiveTask.WordDictionary;
                var columnLenght = words.Count / 3;
                WordsColumn1 = words.Take(columnLenght).ToList();
                WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
                WordsColumn3 = words.Skip(columnLenght * 2).ToList();
                Tests = ActiveTask.Tests;
            }            
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public List<WordDictionary> WordsColumn1 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn2 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn3 { get; set; } = new List<WordDictionary>();

        private ProgressAlfabet? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public AlfabetTask ActiveTask { get; set; }


        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;

            var userId = _authService.GetUserId();
            
            if(!userId.HasValue)
                return;
            
            ActiveTask = await _applicationDB.Set<AlfabetTask>().Include(x=>x.WordDictionary)
                .Include(p => p.ProgressAlfabet.Where(a => a.User.Id == userId))
                .FirstAsync(x => x.Name == taskName);
            var words = ActiveTask.WordDictionary;
            var columnLenght = words.Count/3;
            WordsColumn1 = words.Take(columnLenght).ToList();   
            WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
            WordsColumn3 = words.Skip(columnLenght * 2).ToList();
            Tests = ActiveTask.Tests;
            _progressCurrentTask = ActiveTask.ProgressAlfabet.FirstOrDefault();


            _userService.StoreValueInSession("ActiveAlphabetTask", ActiveTask.Id.ToString());
        }

        public async Task OnPostTestsSubmittedAsync(IFormCollection data)
        {
            var correctAnswerCount = 0;

            foreach (var testResult in data)
            {
                if(!int.TryParse(testResult.Key, out var testId))
                {
                    continue;
                }

                var testAnswer = testResult.Value;
                var test = Tests.First(x => x.Id == testId);
                if(test.QuestionAnswer == testAnswer)
                {
                    correctAnswerCount++;
                }
            }   

            var totalTestsCount = Tests.Count();
            var progress = (correctAnswerCount * 100) / totalTestsCount;
            var userid = _authService.GetUserId();
            var user = _applicationDB.Set<User>().First(x => x.Id == userid);
            if (_progressCurrentTask == null)
            {
                _progressCurrentTask = new ProgressAlfabet() {User=user, AlfabetTask = ActiveTask, scope = progress };
                _applicationDB.Add(_progressCurrentTask);
            }
            else
            {
                _progressCurrentTask.scope = progress;
                _applicationDB.Update(_progressCurrentTask);
            }
            _applicationDB.SaveChanges();

        }
    }
}
