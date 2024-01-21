
namespace TurkishTalk.Persistance.Models
{
    public class WordDictionary
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required string Transcription { get; set; }
        public required string Translation { get; set; }
        public WriteTask? WriteTask { get; set; }
        public AlfabetTask? AlfabetTask { get; set; }
    }
}
