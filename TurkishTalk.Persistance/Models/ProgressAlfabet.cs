using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class ProgressAlfabet
    {
        public int Id { get; set; }
        public int scope { get; set; }
        public required AlfabetTask AlfabetTask { get; set; }
        public required User User { get; set; }

    }
}
