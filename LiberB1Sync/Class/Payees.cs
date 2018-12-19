using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{
    class Payees
    {
        public String id_type{ get; set; }
        public String id{ get; set; }
        public String legal_name{ get; set; }
        public String trade_name{ get; set; }
        public String email{ get; set; }
        public String phone{ get; set; }
        public List<Accounts> accounts = new List<Accounts>();
        public List<Contacts> contacts = new List<Contacts>();

        public Payees(
                 String id_type,
                 String id,
                 String legal_name,
                 String trade_name,
                 String email,
                 String phone,
                 List<Accounts> accounts, 
                 List<Contacts> contacts
            )
        {

            this.id_type = id_type;
            this.id = id;
            this.legal_name = legal_name;
            this.trade_name = trade_name;
            this.email = email;
            this.phone = phone;
            this.accounts = accounts;
            this.contacts = contacts;


        }

    }
}
