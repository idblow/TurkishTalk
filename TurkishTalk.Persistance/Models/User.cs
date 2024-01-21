using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Login { get; set; }
        public required string HeshedPassword { get; set; }

        public required string Salt { get; set; }

        public required byte[] Image { get; set;}

        public List<ProgresWrite>? ProgresWrite { get; set; }
        public List<ProgresGrammar>? ProgresGrammar { get; set; }
        public List<ProgresRead>? ProgresRead { get; set; }
        public List<ProgressAlfabet>? ProgressAlfabet { get; set; }

    }
}
