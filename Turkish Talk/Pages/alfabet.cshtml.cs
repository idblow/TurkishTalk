using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Pages
{
    public class alfabetModel : PageModel
    {
        private readonly ApplicationDBContext _applicationDB;

        public alfabetModel(ApplicationDBContext applicationDB)
        {
            this._applicationDB = applicationDB;
        }
        public List<string> TaskTopics { get; set; } = new List<string>();
        public List<WordDictionary> WordsColumn1 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn2 { get; set; } = new List<WordDictionary>();
        public List<WordDictionary> WordsColumn3 { get; set; } = new List<WordDictionary>();
        public List<TestData> Tests { get; set; } = new List<TestData>();

        public async Task OnGetAsync()
        {
            TaskTopics = await _applicationDB.Set<AlfabetTask>().Select(x=>x.Name).ToListAsync();

            ActiveTask = await _applicationDB.Set<AlfabetTask>().Include(x => x.WordDictionary).FirstAsync();
            var words = ActiveTask.WordDictionary;
            var columnLenght = words.Count / 3;
            WordsColumn1 = words.Take(columnLenght).ToList();
            WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
            WordsColumn3 = words.Skip(columnLenght * 2).ToList();
            Tests = ActiveTask.Tests;
        }

        public AlfabetTask ActiveTask { get; set; }


        public async Task OnTaskSelected(string name)
        {
            ActiveTask = await _applicationDB.Set<AlfabetTask>().Include(x=>x.WordDictionary).FirstAsync(x => x.Name == name);
            var words = ActiveTask.WordDictionary;
            var columnLenght = words.Count/3;
            WordsColumn1 = words.Take(columnLenght).ToList();   
            WordsColumn2 = words.Skip(columnLenght).Take(columnLenght).ToList();
            WordsColumn3 = words.Skip(columnLenght * 2).ToList();
            Tests = ActiveTask.Tests;
        }


    }
}
