
namespace TurkishTalk.Persistance.Models
{
    public class Section
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List< WriteTask> WriteTasks { get; set; }
        public List<ReadTask> ReadTasks { get; set; }
        public List<AlfabetTask> AlfabetTasks { get; set; }  
        public List<GrammarTask> GrammarTasks { get; set; }
    }
}
