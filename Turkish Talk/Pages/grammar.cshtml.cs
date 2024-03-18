using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class grammarModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;
        public grammarModel(ApplicationDBContext applicationDB)
        {
            this._applicationDB = applicationDB;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public string Rule { get; set; }
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public List<TestData> RadioTests { get; set; } = new List<TestData>() { };
        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<GrammarTask>().Select(x => x.Name).ToListAsync();
            ActiveTask = await _applicationDB.Set<GrammarTask>().FirstAsync();
            Tests = ActiveTask.Tests;
            RadioTests = ActiveTask.RadioTests;
            Rule = ActiveTask.Rule;
        }
        public GrammarTask ActiveTask { get; set; }

        public async Task OnTaskSelected(string name)
        {
            ActiveTask = await _applicationDB.Set<GrammarTask>().FirstAsync(x => x.Name == name);
            Tests = ActiveTask.Tests;
        }

    }
}
