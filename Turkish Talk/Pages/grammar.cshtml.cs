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
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private ProgresGrammar? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;

        public grammarModel(ApplicationDBContext applicationDB, AuthService authService, UserService userService)
        {
            _applicationDB = applicationDB;
            _authService = authService;
            _userService = userService;

            var userId = _authService.GetUserId();
            if (userId.HasValue)
            {
                TaskTopics = _applicationDB.Set<GrammarTask>().Select(x => x.Name).ToList();

                var activeTaskQuery = (IQueryable<GrammarTask>)_applicationDB.Set<GrammarTask>();

                var activeTaskId = _userService.GetValueFromSession("ActiveGrammarTaskId");
                if (!string.IsNullOrEmpty(activeTaskId))
                {
                    activeTaskQuery = activeTaskQuery.Where(p => p.Id == int.Parse(activeTaskId));
                }

                ActiveTask = activeTaskQuery.Include(p => p.ProgresGrammars
                    .Where(a => a.User.Id == userId)).FirstOrDefault();

                var activeTaskName = ActiveTask?.Name ?? string.Empty;

                TaskTopics = _applicationDB.Set<GrammarTask>().Where(x => x.Name != activeTaskName).Select(x => x.Name).ToList();

                _progressCurrentTask = ActiveTask.ProgresGrammars.FirstOrDefault();
                _userService.StoreValueInSession("ActiveGrammarTaskId", ActiveTask.Id.ToString());

                Tests = ActiveTask.Tests;
                RadioTests = ActiveTask.RadioTests;
                Rule = ActiveTask.Rule;
            }
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; }
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public List<TestData> RadioTests { get; set; } = new List<TestData>();


        public GrammarTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;

            var userId = _authService.GetUserId();
            if (!userId.HasValue)
                return;

            ActiveTask = await _applicationDB.Set<GrammarTask>().Include(p => p.ProgresGrammars
                .Where(a => a.User.Id == userId)).FirstAsync(x => x.Name == taskName);

            _progressCurrentTask = ActiveTask.ProgresGrammars.FirstOrDefault();

            Tests = ActiveTask.Tests;
            RadioTests = ActiveTask.RadioTests;
            Rule = ActiveTask.Rule;

            _userService.StoreValueInSession("ActiveGrammarTaskId", ActiveTask.Id.ToString());
        }

        public async Task OnPostTestsSubmitted(IFormCollection data)
        {
            var correctAnswerCount = 0;

            foreach (var testResult in data)
            {
                if (!int.TryParse(testResult.Key, out var testId))
                {
                    continue;
                }
                var testAnswer = testResult.Value;
                var test = Tests.First(x => x.Id == testId);
                if (test.QuestionAnswer == testAnswer)
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
            var userid = _authService.GetUserId();
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
