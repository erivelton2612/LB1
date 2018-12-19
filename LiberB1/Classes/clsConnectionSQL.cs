using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1.Classes
{
    
    class clsConnectionSQL
    {
        public clsConnectionSQL(string serverName, string dbName, string dbID, string dbPassword)
        {
            string ConnectionSAP = @"Data Source=" + serverName + "; Initial Catalog=" + dbName + "; " +
                 "User Id=" + dbID + "; Password=" + dbPassword + ";";
            //stringConectionSAP = @"Data Source=" + sap - server + "; Initial Catalog=" + nomeBancoSap + "; " +
            //"User Id=" + sa + "; Password=" + !casale$2018 * +";";
        }

        public string dbName
        {
            get { return dbName; }
        }

        public string stringConnectionSAP
        {
            get { return stringConnectionSAP; }
        }
        
    }
}
