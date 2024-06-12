using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turkish_Talk.Services;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class mailModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private ProgresWrite? _progressCurrentTask;
        public int ProgresSection = 0;
        public int ProgressCurrentTask => _progressCurrentTask?.Score ?? 0;

        public mailModel(ApplicationDBContext applicationDB, AuthService authService, UserService userService)
        {
            _applicationDB = applicationDB;
            _authService = authService;
            _userService = userService;

            var userId = authService.GetUserId();

            if (userId.HasValue)
            {
                TaskTopics = _applicationDB.Set<WriteTask>().Select(x => x.Name).ToList();

                var activeTaskQuery = (IQueryable<WriteTask>)_applicationDB.Set<WriteTask>();

                var activeTaskId = _userService.GetValueFromSession("ActiveMailTaskId");

                if (!string.IsNullOrEmpty(activeTaskId))
                {
                    activeTaskQuery = activeTaskQuery.Where(p => p.Id == int.Parse(activeTaskId));
                }

                ActiveTask = activeTaskQuery.Include(p => p.ProgresWrite
                    .Where(a => a.User.Id == userId)).FirstOrDefault();

                var activeTaskName = ActiveTask?.Name ?? string.Empty;

                TaskTopics = _applicationDB.Set<WriteTask>().Where(x => x.Name != activeTaskName).Select(x => x.Name).ToList();

                Tests = ActiveTask.Tests;
                TestNames = Tests.Select(x => x.Question).ToList();

                _progressCurrentTask = ActiveTask.ProgresWrite.FirstOrDefault();
                Rule = ActiveTask.Rule;
                FixString = ActiveTask.FixString;
                ProgresSection = userService.GetCabinetViewModel().Result.ScoreWrite;

                _userService.StoreValueInSession("ActiveMailTaskId", ActiveTask.Id.ToString());
            }
        }

        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; }
        public List<TestData> Tests { get; set; }
        public TestData ActiveTest { get; set; }
        public List<string> TestNames { get; set; }
        public string FixString { get; set; }

        public WriteTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;

            var userId = _authService.GetUserId();

            if (!userId.HasValue)
                return;

            ActiveTask = await _applicationDB.Set<WriteTask>().Include(p => p.ProgresWrite
                .Where(a => a.User.Id == userId)).FirstAsync(p => p.Name == taskName);
            Tests = ActiveTask.Tests;
            _progressCurrentTask = ActiveTask.ProgresWrite.FirstOrDefault();
            Rule = ActiveTask.Rule;
            FixString = ActiveTask.FixString;

            _userService.StoreValueInSession("ActiveMailTaskId", ActiveTask.Id.ToString());
        }


        public async Task OnPostTestsSubmitted(IFormCollection data)
        {
            var inputString = data["fixstring"].First().TrimStart().TrimEnd().Replace("\r\n", "");
            var inputCorrect = ActiveTask.FixStringCorrect.Equals(inputString, StringComparison.InvariantCultureIgnoreCase);   
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
            var progress = ((correctAnswerCount * 100) / totalTestsCount)/2;
            if (inputCorrect)
            {
                progress += 50;
            }
            var userid = _authService.GetUserId();
            var user = _applicationDB.Set<User>().First(x => x.Id == userid);
            if (_progressCurrentTask == null)
            {
                _progressCurrentTask = new ProgresWrite() { User = user, WriteTask = ActiveTask, Score = progress };
                _applicationDB.Add(_progressCurrentTask);
            }
            else
            {
                _progressCurrentTask.Score = progress;
                _applicationDB.Update(_progressCurrentTask);
            }

            _applicationDB.SaveChanges();

        }
    }
}