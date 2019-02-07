using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{
    class Contacts
    {
        public String name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }

        public Contacts(String name, String email, String phone)
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
        }
    }
}
