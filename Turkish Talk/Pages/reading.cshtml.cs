using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class readingModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly AuthService authService;
        private ProgresRead? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;

        public readingModel(ApplicationDBContext applicationDB, AuthService authService)
        {
            this._applicationDB = applicationDB;
            this.authService = authService;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public string TextReadingExample { get; set; }
        public string QuestionText { get; set; } 
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<ReadTask>().Select(x => x.Name).ToListAsync();
            ActiveTask = await _applicationDB.Set<ReadTask>().FirstAsync();
            Tests = ActiveTask.Tests;
            TextReadingExample = ActiveTask.TextReadingExample;
            QuestionText = ActiveTask.QuestionText;
        }

        public ReadTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if(string.IsNullOrEmpty(taskName))
                return;
            
            ActiveTask = await _applicationDB.Set<ReadTask>().FirstAsync(x=>x.Name == taskName);
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
                if (test.QuestionAnswer == testAnswer)
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
                _progressCurrentTask = new ProgresRead() { User = user, ReadTask = ActiveTask, scope = progress };
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
