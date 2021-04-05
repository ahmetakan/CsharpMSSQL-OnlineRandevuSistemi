using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje
{
    public class DBEntity
    {
        public string ConnectionString { get; private set; }

        public DBEntity()
        {
            ConnectionString = @"Data Source = .\; Initial Catalog = Proje2; Integrated Security = True";
        }
    }
}
