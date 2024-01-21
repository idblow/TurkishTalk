using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkishTalk.Servises.Models;

namespace TurkishTalk.Servises
{
    public interface ICabinetServices
    {
        Cabinet GetCabinet(int userid);
    }
}
