using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{
    class Payer
    {
        public String id_type { get; set; }
        public String id { get; set; }

        public Payer(String id)
        {
            this.id_type = "CNPJ";
            this.id = id;

        }

    }
}
