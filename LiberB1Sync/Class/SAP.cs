using ConnectionLiberB1.Class;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiberB1Sync;
using System.Globalization;

namespace LiberB1Sync.Class
{
    class SAP
    {
        private SQLiteConnection myconn;
        SAPbobsCOM.Company oCompany = null;
        string json = null;
        public bool connect { get; set; }

        public SAP()
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            String key = "trugLk";

            //buscando dados de conexao no banco SQLite
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                myconn.Open();

                //MyLogger.Log("Conectado ao RabbitMQ");

            }
            catch (Exception ex)
            {
                MyLogger.Log("Error 509 - " + ex.Message);
                MessageBox.Show("Error 509 - " + ex.Message);
                return;
            }

            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [servername],[dbname],[sapuser],[sappass],[sqltype],[dbId],[dbPass]" +
                "from[Connection]; ";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();


            //usando os dados de conexao para conectar no SAP
            int retCode = -1;
            string strMsg;

            try
            {
                oCompany = new SAPbobsCOM.Company();
                oCompany.Server = sqlite_datareader.GetString(0);
                oCompany.CompanyDB = sqlite_datareader.GetString(1);
                switch (sqlite_datareader.GetString(4))
                {
                    case "dst_MSSQL": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL; break;
                    case "dst_DB_2": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2; break;
                    case "dst_SYBASE": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE; break;
                    case "dst_MSSQL2005": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005; break;
                    case "dst_MAXDB": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB; break;
                    case "dst_MSSQL2008": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008; break;
                    case "dst_MSSQL2012": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012; break;
                    case "dst_MSSQL2014": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014; break;
                    case "dst_HANADB": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB; break;
                    case "dst_MSSQL2016": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016; break;
                    case "dst_MSSQL2017": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017; break;

                    default:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017; ;
                        break;
                }

                oCompany.UserName = sqlite_datareader.GetString(2);
                oCompany.Password = Cryptography.Decrypt(sqlite_datareader.GetString(3), key);
                oCompany.UseTrusted = false;
                oCompany.DbUserName = sqlite_datareader.GetString(5);
                oCompany.DbPassword = Cryptography.Decrypt(sqlite_datareader.GetString(6), key);

                myconn.Close();

                retCode = oCompany.Connect();

                if (retCode != 0)
                {
                    strMsg = oCompany.GetLastErrorDescription();
                    MyLogger.Log("Não Conectado, verifique as configurações de conexão");
                    this.connect = false;
                }
                else
                {
                    MyLogger.Log("SAP Conectado!");
                    this.connect = true;
                }
            }
            catch (Exception ex)
            {

                MyLogger.Log("Error 504 - " + ex.Message);
                MessageBox.Show("Error 504 - " + ex.Message);
            }

        }

        internal void InvoiceRequested(List<InvoiceRequest> requests)
        {
            //selecionar o primeiro titulo
            //verificar se o titulo confere os dados com o que está no SAP
            //verificar o status
            //false -  retonna titulo nao disponivel
            //verifacar valor e vencimento
            //retornar erro
            //true - alterar o status do titulo para negociado e retornar informação
            //ir para o proximo se é o ultimo sair

            MyLogger.Log("Registrando alterações no SAP");

            foreach (InvoiceRequest r in requests)
            {
                VerifyInvoice(r);
            }
        }

        private void VerifyInvoice(InvoiceRequest r)
        {
            SAPbobsCOM.JournalEntries oJou;
            SAPbobsCOM.JournalEntries_Lines oJouLine;
            Boolean retcode;
            int transId, lineId, intretcode;
            try
            {
                oJou = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries); //todas as transaçõesefetuadas

                string[] array = r.payer_doc_id.Split(new char[] { '-' }, 2);

                transId = Convert.ToInt32(array.GetValue(0).ToString());
                lineId = Convert.ToInt32(array.GetValue(1).ToString());

                retcode = oJou.GetByKey(transId);

                if (!retcode)
                {
                    MyLogger.Log("Erro ao encontrar titulo " + transId.ToString());
                    MessageBox.Show("Erro ao encontrar titulo " + transId.ToString());
                    return;
                }
                oJouLine = oJou.Lines;

                oJouLine.SetCurrentLine(lineId);
                int a = DateTime.Compare(r.due_date, oJouLine.DueDate);
                //if (String.Compare(r.due_date.ToShortDateString(), oJouLine.DueDate.ToShortDateString()) != 0)//verificar vencimento
                if (a!=0)//verificar vencimento
                {
                    MyLogger.Log("A parcela " + lineId + " do título " + transId + " não está com a mesma data de vencimento. O título não está aprovado para negociação");
                    r.Invalidate();
                }
                else if (r.value != oJouLine.Credit)//verificar valor do titulo
                {
                    MyLogger.Log("A parcela " + lineId + " do título " + transId + " não está com a mesma data de vencimento. O título não está aprovado para negociação");
                    r.Invalidate();
                }
                //else if(1=1)//verificar se o titulo esta com o status confirmado --  não é necessario
                //{
                //   // oJouLine.UserFields.Fields.
                //}
                else
                {
                    // alterar para status negociado
                    MyLogger.Log("A parcela " + lineId + " do título " + transId + " está negociada com sucesso!");
                    oJouLine.UserFields.Fields.Item("U_LB_release").Value = "2";
                    //campo para atualizar os dados de pagamento
                    //oJouLine.UserFields.Fields.Item().Value = ;
                    intretcode = oJou.Update();
                    if (intretcode != 0)
                    {
                        MyLogger.Log(oCompany.GetLastErrorDescription());

                    }
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oJou);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oJouLine);
            }
            catch (Exception ex)
            {
                MyLogger.Log("Erro 5034 -" + ex.Message);

            }


        }

        public bool ExportPayments(/*DateTime lasttime*/)
        {

            MyLogger.Log("Exportando títulos...");
            try
            {

                SAPbobsCOM.Recordset oRecordset =
                            this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                String query = "";
                query = query + "select * from dbo.[LB_titulos]";
               
                oRecordset.DoQuery(query);

                List<TitulosSAP> titulos = new List<TitulosSAP>();

                while (!oRecordset.EoF)
                {
                    List<Contacts> conts = new List<Contacts>();

                    SAPbobsCOM.Recordset oRecordset2 =
                            this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    String varname1 = "";
                    varname1 = varname1 + "select *, ROW_NUMBER() over(partition by cardcode order by sort, email,phone) as rownumber " + "\n";
                    varname1 = varname1 + "	from( " + "\n";
                    varname1 = varname1 + "		select distinct  T1.CardCode, " + "\n";
                    varname1 = varname1 + "		isnull(t0.Name,'') +' - '+ isnull(t0.FirstName,'')  as name, " + "\n";
                    varname1 = varname1 + "			isnull(t0.E_MailL,t1.E_Mail) as email, " + "\n";
                    varname1 = varname1 + "			ISNULL(t0.Tel1+ '-','') + ISNULL(t0.Tel2+ ' / ','') + " + "\n";
                    varname1 = varname1 + "				isnull(t1.Phone1+ '-', '')+ISNULL(t1.Phone2,'') as phone, " + "\n";
                    varname1 = varname1 + "		case when isnull(t0.E_MailL,t1.E_Mail) is null then '3' else '1' end sort " + "\n";
                    varname1 = varname1 + "		from OCRD t1 left join OCPR t0 on t0.CardCode = t1.CardCode " + "\n";
                    varname1 = varname1 + "				 " + "\n";
                    varname1 = varname1 + "		where t0.Name like ('%fin%') or t0.E_MailL like ('%fin%') " + "\n";
                    varname1 = varname1 + " " + "\n";
                    varname1 = varname1 + "		UNION " + "\n";
                    varname1 = varname1 + " " + "\n";
                    varname1 = varname1 + "		select distinct  T1.CardCode, " + "\n";
                    varname1 = varname1 + "			isnull(t0.Name,'') +' - '+ isnull(t0.FirstName,''), " + "\n";
                    varname1 = varname1 + "			isnull(t0.E_MailL,t1.E_Mail), " + "\n";
                    varname1 = varname1 + "			ISNULL(t0.Tel1+ '-','') + ISNULL(t0.Tel2+ ' / ','') + " + "\n";
                    varname1 = varname1 + "					isnull(t1.Phone1+ '-', '')+ISNULL(t1.Phone2,''), " + "\n";
                    varname1 = varname1 + "			case when isnull(t0.E_MailL,t1.E_Mail) is null then '3' else '2' end" + "\n";
                    varname1 = varname1 + "				 " + "\n";
                    varname1 = varname1 + "		from OCRD t1 left join OCPR t0 on t0.CardCode = t1.CardCode " + "\n";
                    varname1 = varname1 + " " + "\n";
                    varname1 = varname1 + " " + "\n";
                    varname1 = varname1 + "	) t0 " + "\n";
                    varname1 = varname1 + "	where CardCode = '" + oRecordset.Fields.Item("cardcode").Value + "' " + "\n";
                    varname1 = varname1 + "	order by cardcode, sort, rownumber";

                    oRecordset2.DoQuery(varname1);


                    while (!oRecordset2.EoF)
                    {
                        Contacts cont = new Contacts(oRecordset2.Fields.Item("name").Value + "",
                                                    oRecordset2.Fields.Item("email").Value + "",
                                                    oRecordset2.Fields.Item("phone").Value + ""
                        );

                        conts.Add(cont);

                        oRecordset2.MoveNext();
                    }

                    Payees payee = new Payees(oRecordset.Fields.Item("id_type").Value + "",
                                         oRecordset.Fields.Item("id").Value + "",
                                         oRecordset.Fields.Item("legal_name").Value + "",
                                         null, //trade_name
                                         oRecordset.Fields.Item("email").Value + "",
                                         oRecordset.Fields.Item("phone").Value + "",
                                         null, //accounts
                                         conts
                       );

                    //inserindo CNPJ sacado
                    SAPbobsCOM.Recordset oRecordset3 =
                           this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordset3.DoQuery("select top 1 REPLACE(REPLACE(REPLACE ( TaxIdNum , '.' , '' ), '/',''),'-','') as payer from OADM");

                    //criando o pay_doc_id
                    String payer_doc_id = oRecordset.Fields.Item("transID").Value + "-" + oRecordset.Fields.Item("Line_ID").Value;

                    TitulosSAP titulo = new TitulosSAP(
                                oRecordset3.Fields.Item("payer").Value + "",
                                payee,
                                oRecordset.Fields.Item("value").Value + "",
                                oRecordset.Fields.Item("original_value").Value + "",
                                oRecordset.Fields.Item("currency").Value + "",
                                oRecordset.Fields.Item("due_date").Value + "",
                                oRecordset.Fields.Item("issue_date").Value + "",
                                oRecordset.Fields.Item("tax_doc_id").Value + "",
                                oRecordset.Fields.Item("tax_doc_sec_id").Value + "",
                                oRecordset.Fields.Item("tax_doc_key").Value + "",
                                oRecordset.Fields.Item("installment").Value + "",
                                payer_doc_id,
                                String.Equals(oRecordset.Fields.Item("release").Value + "", "true")
                    );

                    titulos.Add(titulo);

                    oRecordset.MoveNext();
                }

                String invoice = JsonConvert.SerializeObject(titulos, Formatting.Indented);


                json = "{\"batch\": \"" + System.Guid.NewGuid() + "\", \"invoices\":" + invoice + "}";


                System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\LiberB1\\path.txt", json);
               
                oCompany.Disconnect();
                MyLogger.Log("Usuário SAP desconectado.");
                //escrever: SAP desconectador usuário "++"desconectado
                LiberRabbit rabbitsend = new LiberRabbit();
                rabbitsend.Connect();
                rabbitsend.WriteJson(json, "invoice.batch.imported");


                MyLogger.Log("Escrito na fila");

                return true;

                //IConnection connrabbit = CreateConnection(new ConnectionFactory());
                //String queueName = extName + ".input";
                //try
                //{
                //    return WriteMessageOnQueue(json, queueName, connrabbit);
                //}
                //catch (Exception ex )
                //{
                //    MessageBox.Show(ex.Message);
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                MyLogger.Log("Error 508 - " + ex);
                return false;
            }

        }



        //public ConnectionFactory GetConnectionFactory()
        //{
        //    var connectionFactory = new ConnectionFactory
        //    {
        //        //HostName = ConexaoLiber,
        //        //UserName = userLiber,
        //        //Password = PassLiber
        //        HostName = "edi.staging.libercapital.com.br",
        //        UserName = "liber-dev",
        //        Password = "trugLkvsZPjskaAu"
        //    };
        //    return connectionFactory;
        //}

        //public IConnection CreateConnection(ConnectionFactory connectionFactory)
        //{
        //    return connectionFactory.CreateConnection();
        //}

        //public bool WriteMessageOnQueue(string message, string queueName, IConnection connection)
        //{
        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(message));
        //    }

        //    return true;
        //}

    }
}
