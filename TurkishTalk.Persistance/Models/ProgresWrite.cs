using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class ProgresWrite
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public required User User { get; set; }
        public required WriteTask WriteTask { get; set; }

    }
}
