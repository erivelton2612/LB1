using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{
    class Operation
    {

        SAP sap = null;
        public Operation()
        {
            MyLogger.Log("------------------------------------------------------------------------------------------------");
            MyLogger.Log(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
            MyLogger.Log("Iniciando Sincronização...");
            //button4.BackColor = Color.Gray;
            //button4.Refresh();

            MyLogger.Log("Conectando no SAP...");

            sap = new SAP();
            if (sap.connect == true)
            {
                sap.ExportPayments();
            }

            MyLogger.Log("------------------------------------------------------------------------------------------------");
            MyLogger.Log("Aguardando novas iterações..." + Environment.NewLine);
               
            
            //MyLogger.Log("Exportação não efetuada");
            //textResult.Text = "SAP Conectado" + Environment.NewLine;
            
        }
    }


}
