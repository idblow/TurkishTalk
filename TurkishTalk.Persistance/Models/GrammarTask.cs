
namespace TurkishTalk.Persistance.Models
{
    public class GrammarTask
    {
        public int Id { get; set; }
        public int level { get; set; }
        public required string Name { get; set; }

        public required string Rule { get; set; }

        public List<TestData> Tests { get; set; }

        public required Section Section { get; set; }
        public List<ProgresGrammar> ProgresGrammars { get; set; }

     
    }
}
