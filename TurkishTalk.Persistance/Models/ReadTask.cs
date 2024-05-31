
namespace TurkishTalk.Persistance.Models
{
    public class ReadTask
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public required string TextReadingExample { get; set; }
        public required string Name { get; set; }
        public required Section Section { get; set; }
        public List<ProgresRead> ProgresRead { get; set; }
        public List<TestData> Tests {  get; set; }

        public byte[] VoiceExample { get; set; }

        public string VoiceExampleMimeType { get;set; }
    }
}
