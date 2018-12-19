using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiberB1.Classes
{
    class clsConectionSAP
    {
        SAPbobsCOM.Company oCompany = null;
        public clsConectionSAP(string serverName, string dbName, string SAPpassword, string SAPuser)
        {
            int RetCode = -1;
            string ConnStatus;

            try
            {
                oCompany = new SAPbobsCOM.Company();
                oCompany.Server = serverName;
                oCompany.CompanyDB = dbName;
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                oCompany.UserName = SAPuser;
                oCompany.Password = SAPpassword;
                oCompany.UseTrusted = true;

                RetCode = oCompany.Connect();

                if (RetCode != 0)
                {
                    ConnStatus = oCompany.GetLastErrorDescription();
                }
                else
                {
                    ConnStatus = "Conectando a Compania - " + oCompany.CompanyName;
                    //button1.BackColor = Color.Green;
                }
                //MessageBox.Show(strMsg);

            }
            catch (Exception ex)
            {
                ConnStatus = ex.Message;
            }
        }

        public string ConnStatus { get; set; }
        public int RetCode { get; set; }

    }
}
