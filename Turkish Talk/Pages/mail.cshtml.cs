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
        private readonly AuthService authService;
        private ProgresGrammar? _progressCurrentTask;
        public int ProgressCurrentTask => _progressCurrentTask?.scope ?? 0;

        public mailModel(ApplicationDBContext applicationDB, AuthService authService)
        {
            this._applicationDB = new ApplicationDBContext();
            this.authService = authService;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; } 

        public string FixString { get; set; }

        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<WriteTask>().Select(x => x.Name).ToListAsync();
            ActiveTask = await _applicationDB.Set<WriteTask>().FirstAsync();
            Rule = ActiveTask.Rule;
            FixString = ActiveTask.FixString;
        }
        public WriteTask ActiveTask { get; set; }
        public async Task OnPostTaskSelectedAsync(string taskName)
        {
            if(string.IsNullOrEmpty(taskName))
                return;
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