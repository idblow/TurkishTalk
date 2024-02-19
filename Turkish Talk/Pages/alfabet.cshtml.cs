using Microsoft.AspNetCore.Mvc;
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
        public List<AlfabetTask> Tasks { get; set; }

        public async Task OnGetAsync()
        {
           Tasks = await _applicationDB.Set<AlfabetTask>().ToListAsync();
        }

        public AlfabetTask ActiveTask { get; set; }

        public async Task OnTaskSelected(long id)
        {
            ActiveTask = await _applicationDB.Set<AlfabetTask>().FirstAsync(x => x.Id == id);
        }
    }
}
