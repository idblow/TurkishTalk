using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class WriteTask
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public required string Rule { get; set; }

        public required string  FixString { get; set; }

        public required string FixStringCorrect { get; set; }

        public required Section Section { get; set; }

        public List<ProgresWrite> ProgresWrite { get; set; }

        public List< WordDictionary> WordDictionary { get; set; }
    }
}
