using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class mailModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        public mailModel(ApplicationDBContext applicationDB)
        {
            this._applicationDB = new ApplicationDBContext();
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public List<string> Rule { get; set; } = new List<string>();

        public List<string> FixString { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<WriteTask>().Select(x => x.Name).ToListAsync();
            Rule = await _applicationDB.Set<WriteTask>().Select(x => x.Rule).ToListAsync();
            FixString = await _applicationDB.Set<WriteTask>().Select(x => x.FixString).ToListAsync();
        }

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