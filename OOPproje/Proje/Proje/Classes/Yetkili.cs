using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje
{
    public class Yetkili : Kullanici
    {
        public DateTime DogumTarihi { get; set; }
        private long TCkimliknumarasi { get; set; }

        public long TCkimlikNumarasi
        {
            get
            {
                return TCkimliknumarasi;
            }

            set
            {
                TCkimliknumarasi = value;
            }
        }
    }
}
