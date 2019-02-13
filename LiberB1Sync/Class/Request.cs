using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiberB1Sync.Class
{
    class Request
    {

        public List<InvoiceRequest> requests = new List<InvoiceRequest>();

        public Request()
        {

            MyLogger.Log("------------------------------------------------------------------------------------------------");
            MyLogger.Log(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
            MyLogger.Log("Liber está enviando informações...");

            LiberRabbit listen = new LiberRabbit();
            listen.Connect();
            
            String req;
            
            do
            {
                req = listen.ListenQueueRequest();//producao

                if ( !String.IsNullOrEmpty(req))
                {
                    //para efeito de teste
                    //req = "{ " + "\n";//teste
                    //req = req + "	\"payee\": { " + "\n";//teste
                    //req = req + "		\"id_type\": \"CNPJ\", " + "\n";//teste
                    //req = req + "		\"id\": \"26961015000101\", " + "\n";//teste
                    //req = req + "		\"name\": \"Nome do Fornecedor Ltda\" " + "\n";//teste
                    //req = req + "	}, " + "\n";//teste
                    //req = req + "	\"payer_doc_id\": \"238016-0\", " + "\n";//teste
                    //req = req + "	\"value\": \"892.50\", " + "\n";//teste
                    //req = req + "	\"currency\": \"BRL\", " + "\n";//teste
                    //req = req + "	\"due_date\": \"2018-11-29\" " + "\n";//teste
                    //req = req + "}";//teste

                    dynamic jsonObj = JsonConvert.DeserializeObject(req);

                    String payer_doc_id = jsonObj["payer_doc_id"];
                    double value = Convert.ToDouble(jsonObj["value"]);
                    String aux = jsonObj["due_date"];
                    DateTime due_date = DateTime.ParseExact(aux, "yyyy-MM-dd",
                                               System.Globalization.CultureInfo.InvariantCulture);

                    InvoiceRequest requested = new InvoiceRequest(payer_doc_id, value, due_date);
                    requests.Add(requested);

                    listen.Connect();
                }

                //req = null;//teste
            }
            while (!String.IsNullOrEmpty(req));

            if (requests.Count > 0)
            {
                SAP connSAP = new SAP();
                connSAP.InvoiceRequested(requests);
            }
            else
            {
                MyLogger.Log("Sem títulos para atualizar");
            }
            MyLogger.Log("------------------------------------------------------------------------------------------------");
            MyLogger.Log("Aguardando novas iterações...");
        }
    }
}
