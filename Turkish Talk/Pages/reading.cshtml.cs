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
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private ProgresRead? _progressCurrentTask;
        public int ProgresSection=0;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;

        public readingModel(ApplicationDBContext applicationDB, AuthService authService, UserService userService)
        {
            _applicationDB = applicationDB;
            _authService = authService;
            _userService = userService;

            var userId = authService.GetUserId();

            if (userId.HasValue)
            {

                var activeTaskQuery = (IQueryable<ReadTask>)_applicationDB.Set<ReadTask>();

                var activeTaskId = _userService.GetValueFromSession("ActiveReadingTaskId");
                if (!string.IsNullOrEmpty(activeTaskId))
                {
                    activeTaskQuery = activeTaskQuery.Where(p => p.Id == int.Parse(activeTaskId));
                }

                ActiveTask = activeTaskQuery.Include(p => p.ProgresRead
                    .Where(a => a.User.Id == userId)).FirstOrDefault();

                var activeTaskName = ActiveTask?.Name ?? string.Empty;

                TaskTopics = _applicationDB.Set<ReadTask>().Where(x => x.Name != activeTaskName).Select(x => x.Name).ToList();

                _progressCurrentTask = ActiveTask?.ProgresRead.FirstOrDefault();
                Tests = ActiveTask?.Tests ?? new List<TestData>();
                TextReadingExample = ActiveTask?.TextReadingExample;
                ProgresSection = userService.GetCabinetViewModel().Result.ScoreRead;

                _userService.StoreValueInSession("ActiveReadingTaskId", ActiveTask?.Id.ToString());
            }
        }

        public List<string> TaskTopics { get; set; } = new List<string>();
        public string TextReadingExample { get; set; }
        
        public List<TestData> Tests { get; set; } = new List<TestData>();


        public ReadTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;

            var userId = _authService.GetUserId();
            if (!userId.HasValue)
                return;

            ActiveTask = await _applicationDB.Set<ReadTask>().Include(p => p.ProgresRead
                .Where(a => a.User.Id == userId)).FirstAsync(x => x.Name == taskName);

            _progressCurrentTask = ActiveTask.ProgresRead.FirstOrDefault();
            Tests = ActiveTask.Tests;
            TextReadingExample = ActiveTask.TextReadingExample;

            _userService.StoreValueInSession("ActiveReadingTaskId", ActiveTask.Id.ToString());
        }

        public async Task OnPostTestsSubmittedAsync(IFormCollection data)
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