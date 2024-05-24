using Microsoft.EntityFrameworkCore;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Services
{
    public class ProgresService
    {
        private readonly ApplicationDBContext _applicationDB;
        private readonly HttpContext _httpContext;

        public ProgresService(ApplicationDBContext applicationDB, IHttpContextAccessor httpContext)
        {
            _applicationDB = applicationDB;

            _httpContext = httpContext.HttpContext!;
        }

        public async Task<List<ReadTask>> GetReadTasksAsync()
        {
            return await _applicationDB.Set<ReadTask>().ToListAsync();    
        }

        public async Task<List<WriteTask>> GetWriteTasksAsync()
        {
            return await _applicationDB.Set<WriteTask>().ToListAsync();
        }

        public async Task<List<GrammarTask>> GetGrammarTasksAsync()
        {
            return await _applicationDB.Set<GrammarTask>().ToListAsync();
        }

        public async Task<List<AlfabetTask>> GetAlfabetTasksAsync()
        {
            return await _applicationDB.Set<AlfabetTask>().ToListAsync();
        }

        public async Task<bool> CheckingWriteResponsesAsync(string answer)
        {

            var write_task_result = await _applicationDB.Set<WriteTask>().AnyAsync(x => x.FixStringCorrect == answer);

            if (!write_task_result)
            {
                return false;
            }

            return true;
        }
        //public async Task<bool> CheckingReadResponsesAsync(string answer)
        //{
        //    var read_task_result = await _applicationDB.Set<ReadTask>().AnyAsync(x => x.FixStringCorrect == answer);
            
        //    if(!read_task_result)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public async Task<bool> CheckingTestAsync(int questionId, int selectedAnswerId)
        {
            var correctAnswerId = _applicationDB.Set<WriteTask>().Where(x => x.Id == questionId).Select(x => x.Id).FirstOrDefault();

            return selectedAnswerId == correctAnswerId;
        }

        public async Task<bool> CheckWordAsync(int selectedAnswerId)
        {
            var correctAnswerId = _applicationDB.Set<WordDictionary>().Where(x => x.Id == selectedAnswerId).FirstOrDefault();

            if(correctAnswerId == null)
            {
                return false; 
            }

            return true;
        }

     

      
    }
}
