﻿namespace Turkish_Talk.Models
{
    public class PersonalCabinetViewModel
    {
        public string FullName { get; set; }

        public int TotalScore { get; set; }
        public int Id { get; set; }
        public int ScoreWrite { get; set; }
        public int ScoreRead { get; set; }
        public int ScoreGrammar { get; set; }
        public int ScoreAlphabet { get; set; }
        public bool IsAdmin { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageContentType { get; set; }

    }
}
