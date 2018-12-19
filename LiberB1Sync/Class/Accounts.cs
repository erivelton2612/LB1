using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{
    class Accounts
    {
        public String bank { get; set; }
        public String branch { get; set; }
        public String branch_cd { get; set; }
        public String account { get; set; }
        public String account_cd { get; set; }
        
        public Accounts(String bank, String branch,
                    String branch_cd,
                    String account,
                    String account_cd )
        {
            this.bank = bank;
            this.branch = branch;
            this.branch_cd = branch_cd;
            this.account = account;
            this.account_cd = account_cd;
        }

    }
}
