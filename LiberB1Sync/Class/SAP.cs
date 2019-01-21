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
                MyLogger.Log("Error 502 - " + ex.Message);
                MessageBox.Show("Error 502 - " + ex.Message);
            }

            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [servername],[dbname],[sapuser],[sappass],[sqltype]" +
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
                oCompany.UseTrusted = true;

                myconn.Close();

                retCode = oCompany.Connect();

                if (retCode != 0)
                {
                    strMsg = oCompany.GetLastErrorDescription();
                    MessageBox.Show(strMsg);
                }

                MyLogger.Log("SAP Conectado!");

                //lendo outros parametros
                //sqlite_cmd = myconn.CreateCommand();
                //sqlite_cmd.CommandText = "SELECT [Id],[QueueName],[OriginDoc],[TimeSAP],[FieldChaveAcesso],[TimeStartSAP],[TimeStopSAP],[Import]"+
                //                           "FROM[Configuration];";

                //sqlite_datareader = sqlite_cmd.ExecuteReader();
                //sqlite_datareader.Read();

                //MessageBox.Show(sqlite_datareader.GetString(7));


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

                if (String.Compare(r.due_date.ToShortDateString(), oJouLine.DueDate.ToShortDateString()) != 0)//verificar vencimento
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
                    oJouLine.UserFields.Fields.Item("U_LB_release").Value = "1";
                    intretcode = oJou.Update();
                    if (intretcode != 0)
                    {
                        MyLogger.Log(oCompany.GetLastErrorDescription());

                    }
                }
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
                query = query + "SELECT DISTINCT " + "\n";
                query = query + "	REPLACE(REPLACE(REPLACE ( T9.cnpjFornecedor , '.' , '' ), '/',''),'-','') as id, " + "\n";
                query = query + "	t9.type id_type, " + "\n";
                query = query + "	T2.[CardName] legal_name, " + "\n";
                query = query + "	t2.E_Mail as email, " + "\n";
                query = query + "	t2.Phone1 + ' - ' + t2.Phone2 + ' - ' +t2.Cellular as phone, " + "\n";
                query = query + "	t2.CardCode, " + "\n";
                query = query + "	T1.[BalDueCred]  - ISNULL( " + "\n";
                query = query + "	(SELECT SUM(TT7.WTAMNT/ISNULL(TT8.INSTNUM,1)) " + "\n";
                query = query + "		 FROM JDT1 TT1 " + "\n";
                query = query + "		 INNER JOIN OCRD TT2 ON TT1.ShortName = TT2.CardCode " + "\n";
                query = query + "		 LEFT JOIN VPM2 TT3 ON TT1.TransId = TT3.DocNum AND TT3.InvType = 18 " + "\n";
                query = query + "		 INNER JOIN OPCH TT6 ON TT1.SOURCEID = TT6.DOCNUM AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) " + "\n";
                query = query + "		 INNER JOIN PCH5 TT7 ON TT7.ABSENTRY = TT6.DOCENTRY AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) " + "\n";
                query = query + "		 INNER JOIN OCTG TT8 ON TT8.GROUPNUM = TT6.GROUPNUM  AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) " + "\n";
                query = query + "		 WHERE Substring(TT1.[ShortName],1,1) = 'F' " + "\n";
                query = query + "		 AND (TT1.[TransType] = 30 or TT1.[TransType]=18 or TT1.[TransType]=204) " + "\n";
                query = query + "		 AND SUBSTRING (TT1.[Account] ,1,1)= '2' " + "\n";
                query = query + "		 AND TT1.[Credit]> 0 " + "\n";
                query = query + "		 AND TT1.BALDUECRED <>0 " + "\n";
                query = query + "		 AND TT1.[ShortName] = T1.[ShortName] " + "\n";
                query = query + "		 AND TT1.[TransType] = T1.[TransType] " + "\n";
                query = query + "		 AND  TT1.[TransId]= T1.[TransId] " + "\n";
                query = query + "		 AND TT7.[Category] = 'P' " + "\n";
                query = query + "		 --AND TT1.[DueDate]>= @dtin and TT1.[DueDate]<= @dtfi " + "\n";
                query = query + "	)/(ISNULL(T8.INSTNUM,1)),0)  as value, " + "\n";
                query = query + "	T1.[Credit] as original_value, " + "\n";
                query = query + "	substring(CONVERT(varchar,T1.[DueDate],126),0,11) AS due_date, " + "\n";
                query = query + "	substring(CONVERT(varchar,t1.refdate ,126),0,11) as issue_date, " + "\n";
                query = query + "	T6.Serial as tax_doc_id, " + "\n";
                query = query + "	t6.SeriesStr as tax_doc_sec_id, " + "\n";
                query = query + "	isnull(t6.U_chaveacesso,'') as tax_doc_key, " + "\n";
                query = query + "	[Ref3Line] installment, " + "\n";
                query = query + "	(select top 1 isnull(c1.ISOCurrCod,(select top 1 z1.ISOCurrCod from OADM z0 left join OCRN z1 on z0.SysCurrncy = z1.CurrCode)) " + "\n";
                query = query + "		from JDT1 C0 left join OCRN C1 on c0.FCCurrency = c1.CurrCode ) as currency, " + "\n";
                query = query + "	t1.TransId, " + "\n";
                query = query + "	t1.Line_ID, " + "\n";
                query = query + "	case when (t1.U_lb_release = 1) then 'true' else 'false' end as release " + "\n";
                query = query + "	--'lastchange' as lastchange  " + "\n";
                query = query + " " + "\n";
                query = query + "	FROM JDT1 T1 " + "\n";
                query = query + "	INNER JOIN OCRD T2 ON T1.ShortName = T2.CardCode " + "\n";
                query = query + "	LEFT JOIN VPM2 T3 ON T1.TransId = T3.DocNum AND (T3.InvType <> 30 and T3.InvType <> 18  and T3.InvType <> 204 ) " + "\n";
                query = query + "	LEFT JOIN OPCH T6 ON T1.SOURCEID = T6.DocEntry AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30) " + "\n";
                query = query + "	LEFT JOIN PCH5 T7 ON T7.ABSENTRY = T6.DOCENTRY AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30) " + "\n";
                query = query + "	LEFT JOIN OCTG T8 ON T8.GROUPNUM = T6.GROUPNUM  AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30) " + "\n";
                query = query + "	LEFT JOIN (SELECT max( ISNULL (T0.TaxId0 , T0.TaxId4) ) as cnpjFornecedor ,T0.CARDCODE, case when T0.TaxId0 is not null then 'CNPJ' else 'CPF' end as type " + "\n";
                query = query + "				FROM CRD7 T0 " + "\n";
                query = query + "				where t0.CardCode like 'F%' and (T0.TaxId0 is not null or T0.TaxId4 is not null) " + "\n";
                query = query + "				GROUP BY T0.CARDCODE, T0.TaxId0) as T9 ON T9.CARDCODE = T2.CardCode " + "\n";
                query = query + " " + "\n";
                query = query + "	WHERE " + "\n";
                query = query + "	--(T9.cnpjFornecedor IS NOT NULL AND T9.cnpjFornecedor !='') AND  " + "\n";
                query = query + "	Substring(T1.[ShortName],1,1) = 'F' " + "\n";
                query = query + "	AND T1.[TransType]=18 " + "\n";
                query = query + "	AND SUBSTRING (T1.[Account] ,1,1)= '2' " + "\n";
                query = query + "	AND T1.[Credit]> 0 " + "\n";
                query = query + "	AND T1.BALDUECRED <>0 " + "\n";
                query = query + "	--AND T1.[DueDate] between @dtin AND @dtfi " + "\n";
                query = query + "	--and t1.TransId = 221672 " + "\n";
                query = query + " " + "\n";
                query = query + "	--ORDER BY emailContatoFornecedor " + "\n";
                query = query + "	--ORDER BY DueDater";

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


                //System.IO.File.WriteAllText(@"D:\path.txt", json);

                LiberRabbit rabbitsend = new LiberRabbit();
                rabbitsend.Connect();

                oCompany.Disconnect();
                MyLogger.Log("Usuário SAP desconectado.");
                //escrever: SAP desconectador usuário "++"desconectado



                MyLogger.Log("Escrito na fila");

                rabbitsend.WriteJson(json);
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
