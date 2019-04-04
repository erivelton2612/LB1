using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiberB1Sync.Class
{
    class InvoiceRequest
    {
        public String payer_doc_id { get; set; }
        public double value { get; set; }
        public DateTime due_date { get; set; }
        public bool valid { get; set; }

        public InvoiceRequest(
             String payer_doc_id,
             double value,
             DateTime due_date)
        {
            this.payer_doc_id = payer_doc_id;
            this.value = value;
            this.due_date = due_date;
            valid = true;
        }

        public void Invalidate()
        {
            this.valid = false;
        }

        public void Validate()
        {
            this.valid = true;
        }
    }
}