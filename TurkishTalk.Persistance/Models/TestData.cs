using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class TestData
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string QuestionAnswer { get; set; }
    }
}
