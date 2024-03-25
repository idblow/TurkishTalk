using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class readingModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        public readingModel(ApplicationDBContext applicationDB)
        {
            this._applicationDB = applicationDB;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public List<string> TextReadingExample { get; set; } = new List<string>();
        public List<string> QuestionText { get; set; } = new List<string>();
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<ReadTask>().Select(x => x.Name).ToListAsync();
            TextReadingExample = await _applicationDB.Set<ReadTask>().Select(x=>x.TextReadingExample).ToListAsync();
            QuestionText = await _applicationDB.Set<ReadTask>().Select(x => x.QuestionText).ToListAsync();
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
            foreach (var testResult in data)
            {
                var testId = int.Parse(testResult.Key);
                var testAnswer = testResult.Value;
            }   
        }
    }
}
