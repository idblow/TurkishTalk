using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkishTalk.Servises.Models;

namespace TurkishTalk.Servises
{
    public class CabinetServices : ICabinetServices
    {
        public Cabinet GetCabinet(int userid)
        {
            var mycabinet = new Cabinet();
            mycabinet.Id = 1;
            mycabinet.Fio = "Иван Иванович Иванов";
            mycabinet.TotalScore = 14;
            var kategoria1 = new Kategoria { Id = 1, Title = "Алфавит", Score = 50 };
            var kategoria2 = new Kategoria { Id = 2, Title = "Чтение", Score = 47 };
            var kategoria3 = new Kategoria { Id = 3, Title = "Письмо", Score = 16 };
            var kategoria4 = new Kategoria { Id = 4, Title = "Грамматика", Score = 81 };
            mycabinet.KategoriaList = new List<Kategoria> { kategoria1, kategoria2, kategoria3, kategoria4 };
            return mycabinet;
        }
    }
}
