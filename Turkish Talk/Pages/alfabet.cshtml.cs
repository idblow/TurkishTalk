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
        private readonly AuthService authService;

        public alfabetModel(ApplicationDBContext applicationDB, AuthService authService)
        {
            this._applicationDB = applicationDB;
            this.authService = authService;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public List<WordDictionary> WordsColumn1 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn2 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn3 { get; set; } = new List<WordDictionary>();

        private ProgressAlfabet? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public async Task OnGetAsync()
        {
            var userId = authService.GetUserId();
            if(!userId.HasValue)
            {
                return;
            }
            TaskTopics = await _applicationDB.Set<AlfabetTask>().Select(x=>x.Name).ToListAsync();

            ActiveTask = await _applicationDB.Set<AlfabetTask>().Include(x => x.WordDictionary).Include(x=>x.ProgressAlfabet.Where(y=>y.User.Id==userId)).FirstAsync();
            _progressCurrentTask = ActiveTask.ProgressAlfabet.FirstOrDefault();
            var words = ActiveTask.WordDictionary;
            var columnLenght = words.Count / 3;
            WordsColumn1 = words.Take(columnLenght).ToList();
            WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
            WordsColumn3 = words.Skip(columnLenght * 2).ToList();
            Tests = ActiveTask.Tests;

        }

        public AlfabetTask ActiveTask { get; set; }


        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;
            
            ActiveTask = await _applicationDB.Set<AlfabetTask>().Include(x=>x.WordDictionary).FirstAsync(x => x.Name == taskName);
            var words = ActiveTask.WordDictionary;
            var columnLenght = words.Count/3;
            WordsColumn1 = words.Take(columnLenght).ToList();   
            WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
            WordsColumn3 = words.Skip(columnLenght * 2).ToList();
            Tests = ActiveTask.Tests;
        }

        public async Task OnPostTestsSubmittedAsync(IFormCollection data)
        {
            var correctAnswerCount = 0;

            foreach (var testResult in data)
            {
                var testId = int.Parse(testResult.Key);
                var testAnswer = testResult.Value;
                var test = Tests.First(x => x.Id == testId);
                if(test.QuestionAnswer == testAnswer)
                {
                    correctAnswerCount++;
                }
            }   

            var totalTestsCount = Tests.Count();
            var progress = (correctAnswerCount * 100) / totalTestsCount;
            var userid = authService.GetUserId();
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
