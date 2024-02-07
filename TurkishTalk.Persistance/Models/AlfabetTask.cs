
namespace TurkishTalk.Persistance.Models
{
    public class AlfabetTask
    {
        public int Id { get; set; }
        public int Level { get; set; }

        public required Section Section { get; set; }
        public List<ProgressAlfabet> ProgressAlfabet { get; set; }
        public List< WordDictionary> WordDictionary { get; set; }

        public List<TestData> Tests { get; set; }
    }
}
