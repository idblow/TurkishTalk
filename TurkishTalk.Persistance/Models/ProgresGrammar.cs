
namespace TurkishTalk.Persistance.Models
{
    public class ProgresGrammar
    {
        public int Id { get; set; }

        public int scope { get; set; }

        public required User User { get; set; }

        public required GrammarTask GrammarTask { get; set; }
    }
}
