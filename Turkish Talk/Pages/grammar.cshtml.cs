using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class grammarModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly AuthService authService;
        private ProgresGrammar? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;

        public grammarModel(ApplicationDBContext applicationDB, AuthService authService)
        {
            this._applicationDB = applicationDB;
            this.authService = authService;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; }
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public List<TestData> RadioTests { get; set; } = new List<TestData>();
        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<GrammarTask>().Select(x => x.Name).ToListAsync();
            ActiveTask = await _applicationDB.Set<GrammarTask>().FirstAsync();
            Tests = ActiveTask.Tests;
            RadioTests = ActiveTask.RadioTests;
            Rule = ActiveTask.Rule;
        }
        public GrammarTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if(string.IsNullOrEmpty(taskName))
                return;
            
            ActiveTask = await _applicationDB.Set<GrammarTask>().FirstAsync(x => x.Name == taskName);
            Tests = ActiveTask.Tests;
        }

        public async Task OnPostTestsSubmitted(IFormCollection data)
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
                _progressCurrentTask = new ProgresGrammar() { User = user, GrammarTask = ActiveTask, scope = progress };
                _applicationDB.Add(_progressCurrentTask);
            }
            else
            {
                _progressCurrentTask.scope = progress;
                _applicationDB.Update(_progressCurrentTask);
            }
            _applicationDB.SaveChanges();
        }

        public async Task OnPostRadioTestsSubmittedAsync(IFormCollection data)
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
                _progressCurrentTask = new ProgresGrammar() { User = user, GrammarTask = ActiveTask, scope = progress };
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
