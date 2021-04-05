using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje
{
    public class Kullanici
    {
        private string isim { get; set; }

        public string Isim
        {
            get
            {
                return isim;
            }

            set
            {
                isim = value;
            }
        }

        private string soyIsim { get; set; }

        public string SoyIsim 
        { 
            get
            {
                return soyIsim;
            }

            set
            {
                soyIsim = value;
            }
        }

        public long TelefonNumarasi { get; set; }
        public Adres Adres { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Eposta { get; set; }

        public Kullanici()
        {
            Adres = new Adres();
        }
    }
}
