
namespace TurkishTalk.Persistance.Models
{
    public class ProgresRead
    {
        public int Id { get; set; }
        public int scope { get; set; }

        public required ReadTask ReadTask { get; set; }
        public required User User { get; set; }
    }
}
