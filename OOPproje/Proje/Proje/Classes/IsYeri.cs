using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje
{
    public class IsYeri
    {
        public int ID { get; set; }
        public string Isim { get; set; }
        public string IsYeriTuru { get; set; }
        public long TelefonNumarasi { get; set; }
        public Adres adres { get; set; }

        public IsYeri()
        {
            adres = new Adres();
        }

    }
}
