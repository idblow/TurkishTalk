using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Servises.Models
{
    public class Cabinet
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Img { get; set; }
        public int TotalScore { get; set; }

        public List<Kategoria> KategoriaList { get; set; }
    }
}
