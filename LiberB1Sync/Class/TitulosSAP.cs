using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberB1Sync.Class
{

    class TitulosSAP
    {
        public Payer payer { get; set; }
        public Payees payee { get; set; }
        public String value { get; set; }
        public String original_value { get; set; }
        public String currency { get; set; }
        public String due_date { get; set; }
        public String issue_date { get; set; }
        public String tax_doc_id { get; set; }
        public String tax_doc_sec_id { get; set; }
        public String tax_doc_key { get; set; }
        public String installment { get; set; }
        public String payer_doc_id { get; set; }
        public bool release { get; set; }
        

        public TitulosSAP(String payer,
             Payees payee ,
             String value,
             String original_value ,
             String currency ,
             String due_date ,
             String issue_date ,
             String tax_doc_id ,
             String tax_doc_sec_id ,
             String tax_doc_key ,
             String installment ,
             String payer_doc_id,
             bool release 
            )
        {
            this.payer = new Payer(payer);
            this.payee = payee;
            this.value = value;
            this.original_value = original_value;
            this.currency = currency;
            this.due_date = due_date;
            this.issue_date = issue_date;
            this.tax_doc_id = tax_doc_id;
            this.tax_doc_sec_id = tax_doc_sec_id;
            this.tax_doc_key = tax_doc_key;
            this.installment = installment;
            this.payer_doc_id = payer_doc_id;
            this.release = release;

        }

            //"cnpjFornecedor" + oRecordset.Fields.Item("cnpjFornecedor").Value + " - " +
                    //"cardname" + oRecordset.Fields.Item("cardname").Value + " - " +
                    //"nomeContatoFornecedor" + oRecordset.Fields.Item("nomeContatoFornecedor").Value + " - " +
                    //"emailContatoFornecedor" + oRecordset.Fields.Item("emailContatoFornecedor").Value + " - " +
                    //"valorTitulo" + oRecordset.Fields.Item("valorTitulo").Value + " - " +
                    //"vencimentoTitulo" + oRecordset.Fields.Item("vencimentoTitulo").Value + " - " +
                    //"numeroTitulo" + oRecordset.Fields.Item("numeroTitulo").Value + " - " +
                    //"chaveNF" + oRecordset.Fields.Item("chaveacesso").Value + " - " +
                    //"numeroNF" + oRecordset.Fields.Item("numeroNF").Value + " - " +
                    //"serieNF" + oRecordset.Fields.Item("serieNF").Value + " - " +
                    //"cardcode" + oRecordset.Fields.Item("cardcode").Value + " - " +
                    //"transID" + oRecordset.Fields.Item("transID").Value + " - " +
                    //"LineID" + oRecordset.Fields.Item("line_ID").Value + " - "
    }
}
