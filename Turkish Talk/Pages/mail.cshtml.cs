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
                    .Where(a => a.User.Id == userId)).First();

                _progressCurrentTask = ActiveTask.ProgresWrite.FirstOrDefault();
                Rule = ActiveTask.Rule;
                FixString = ActiveTask.FixString;
                
                
                _userService.StoreValueInSession("ActiveMailTaskId", ActiveTask.Id.ToString());
            }
        }

        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; }

        public string FixString { get; set; }

        public WriteTask ActiveTask { get; set; }

        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return;

            var userId = _authService.GetUserId();
            
            if(!userId.HasValue)
                return;

            ActiveTask = await _applicationDB.Set<WriteTask>().Include(p => p.ProgresWrite
                .Where(a => a.User.Id == userId)) .FirstAsync(p => p.Name == taskName);
                
            _progressCurrentTask = ActiveTask.ProgresWrite.FirstOrDefault();
            Rule = ActiveTask.Rule;
            FixString = ActiveTask.FixString;
            
            _userService.StoreValueInSession("ActiveMailTaskId", ActiveTask.Id.ToString());
        }

        public async Task OnPostTestsSubmitted(IFormCollection data)
        {
            foreach (var testResult in data)
            {
                var testId = int.Parse(testResult.Key);
                var testAnswer = testResult.Value;
            }
        }

        public async Task OnPostRadioTestsSubmittedAsync(IFormCollection data)
        {
            foreach (var testResult in data)
            {
                var testId = int.Parse(testResult.Key);
                var testAnswer = testResult.Value;
            }
        }
    }
}