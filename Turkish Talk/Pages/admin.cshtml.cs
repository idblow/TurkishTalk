using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;


namespace Turkish_Talk.Pages
{
    public record UserPresentation(int Id, string FullName, int TotalProgress);

    public class adminModel : PageModel
    {

        private ApplicationDBContext _applicationDBContext;

        public adminModel(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            Init();
        }

        public List<UserPresentation> Users { get; set; } = new List<UserPresentation>();
        public List<WriteTask> WriteTasks { get; set; }
        public List<ReadTask> ReadTasks { get; set; }
        public List<AlfabetTask> AlfabetTasks { get; set; }
        public List<GrammarTask> GrammarTasks { get; set; }

        private void Init()
        {
            WriteTasks = _applicationDBContext.Set<WriteTask>().ToList();
            ReadTasks = _applicationDBContext.Set<ReadTask>().ToList();
            AlfabetTasks = _applicationDBContext.Set<AlfabetTask>().ToList();
            GrammarTasks = _applicationDBContext.Set<GrammarTask>().ToList();

            var users = _applicationDBContext.Set<User>()
                .Include(x => x.ProgressAlfabet)
                .Include(x => x.ProgresGrammar)
                .Include(x => x.ProgresRead)
                .Include(x => x.ProgresWrite)
                .ToList();

            var totalTaskCount = WriteTasks.Count() + ReadTasks.Count() + AlfabetTasks.Count() + GrammarTasks.Count();
            var totalPoints = totalTaskCount * 100;

            foreach (var user in users)
            {
                var userTotalPoints = 0;
                userTotalPoints += user.ProgressAlfabet.Select(x => x.scope).Sum();
                userTotalPoints += user.ProgresWrite.Select(x => x.Score).Sum();
                userTotalPoints += user.ProgresRead.Select(x => x.scope).Sum();
                userTotalPoints += user.ProgresGrammar.Select(x => x.scope).Sum();

                var userProgress = (userTotalPoints / totalPoints) * 100;

                Users.Add(new UserPresentation(user.Id, user.FullName, userProgress));
            }

        }
        public async Task OnPostAlphabetTaskAddAsync(IFormCollection form)
        {
            var level = int.Parse(form["complex"].First());
            var course = int.Parse(form["course"].First());
            var testjson = JsonConvert.DeserializeObject<List<TestData>>(form["testjson"].First());
            var topic = form["topic"].First();

            var section = await _applicationDBContext.Set<Section>().FirstAsync(x => x.Id == course);

            var model = new AlfabetTask
            {
                Level = level,
                Section = section,
                Tests = testjson,
                Name = topic
            };

            _applicationDBContext.Add(model);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostAlphabetTaskRemoveAsync(IFormCollection form)
        {
            var topic = form["topic"].First();

            var task = await _applicationDBContext.Set<AlfabetTask>().FirstAsync(x => x.Name == topic);

            _applicationDBContext.Remove(task);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task OnPostReadTaskAddAsync(string topic, string complex, string course, IFormFile voiceexample, string text, string testjson)
        {


            var level = int.Parse(complex);
            var courseParsed = int.Parse(course);
            var test = JsonConvert.DeserializeObject<List<TestData>>(testjson);


            var section = await _applicationDBContext.Set<Section>().FirstAsync(x => x.Id == courseParsed);

            var model = new ReadTask
            {
                Level = level,
                Section = section,
                Tests = test,
                Name = topic,
                TextReadingExample = text,
                VoiceExample = voiceexample.OpenReadStream().ToArray(),
                VoiceExampleMimeType = voiceexample.ContentType
            };

            _applicationDBContext.Add(model);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task OnPostReadTaskRemoveAsync(IFormCollection form)
        {
            var topic = form["topic"].First();

            var task = await _applicationDBContext.Set<ReadTask>().FirstAsync(x => x.Name == topic);

            _applicationDBContext.Remove(task);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task OnPostWriteTaskAddAsync(IFormCollection form)
        {
            var level = int.Parse(form["complex"].First());
            var course = int.Parse(form["course"].First());
            var testjson = JsonConvert.DeserializeObject<List<TestData>>(form["testjson"].First());
            var topic = form["topic"].First();
            var rule = form["rule"].First();
            var fixstring = form["fixstring"].First();
            var fixstringcorrect = form["fixstringcorrect"].First();

            var section = await _applicationDBContext.Set<Section>().FirstAsync(x => x.Id == course);

            var model = new WriteTask
            {
                Level = level,
                Section = section,
                Tests = testjson,
                Name = topic,
                Rule = rule,
                FixString = fixstring,
                FixStringCorrect = fixstringcorrect
            };

            _applicationDBContext.Add(model);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostWriteTaskRemoveAsync(IFormCollection form)
        {
            var topic = form["topic"].First();

            var task = await _applicationDBContext.Set<WriteTask>().FirstAsync(x => x.Name == topic);

            _applicationDBContext.Remove(task);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostGrammarTaskAddAsync(IFormCollection form)
        {
            var level = int.Parse(form["complex"].First());
            var course = int.Parse(form["course"].First());
            var testjson = JsonConvert.DeserializeObject<List<TestData>>(form["testjson"].First());
            var topic = form["topic"].First();
            var rule = form["rule"].First();
            var radiotestjson = JsonConvert.DeserializeObject<List<TestData>>(form["radiotestjson"].First());

            var section = await _applicationDBContext.Set<Section>().FirstAsync(x => x.Id == course);

            var model = new GrammarTask
            {
                level = level,
                Section = section,
                Tests = testjson,
                Name = topic,
                Rule = rule,
                RadioTests = radiotestjson
            };

            _applicationDBContext.Add(model);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostGrammarTaskRemoveAsync(IFormCollection form)
        {
            var topic = form["topic"].First();

            var task = await _applicationDBContext.Set<GrammarTask>().FirstAsync(x => x.Name == topic);

            _applicationDBContext.Remove(task);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task OnPostDictionaryAddAsync(IFormCollection form)
        {
            var word = form["word"].First();
            var translation = form["translation"].First();
            var topicwritting = int.Parse(form["topicwritting"].First());
            var topicalphabet = int.Parse(form["topicalfabet"].First());

            var topicWriting = await _applicationDBContext.Set<WriteTask>().FirstOrDefaultAsync(t => t.Id == topicwritting);
            var topicAlphabet = await _applicationDBContext.Set<AlfabetTask>().FirstOrDefaultAsync(t => t.Id == topicalphabet);

            var model = new WordDictionary
            {
                Word = word,
                WriteTask = topicWriting,
                AlfabetTask = topicAlphabet,
                Translation = translation,
            };

            _applicationDBContext.Add(model);

            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task OnPostDictionaryTaskRemoveAsync(IFormCollection form)
        {
            var word = form["word"].First();

            var task = await _applicationDBContext.Set<WordDictionary>().FirstAsync(x => x.Word == word);

            _applicationDBContext.Remove(task);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task OnPostUserRemoveAsync(IFormCollection form)
        {
            var userId = int.Parse(form["userid"].First());

            await _applicationDBContext.Set<User>().Where(x => x.Id == userId).ExecuteDeleteAsync();

        }
        public void OnGet()
        {
        }
    }
}
