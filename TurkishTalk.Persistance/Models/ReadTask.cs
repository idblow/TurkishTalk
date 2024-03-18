﻿
namespace TurkishTalk.Persistance.Models
{
    public class ReadTask
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public required string TextReadingExample { get; set; }
        public required string TextReadingVoiceExample { get; set; }
        //public required string QuestionsData {  get; set; }
        public required string FixString { get; set; }
        public required string QuestionText { get; set;}
        public required string FixStringCorrect { get; set;}
        public required string Name { get; set; }
        public required Section Section { get; set; }
        public List<ProgresRead> ProgresRead { get; set; }
        public List<TestData> Tests {  get; set; }
    }
}
