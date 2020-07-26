using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceDate { get; set; }
        public int CustmerId { get; set; }
        public string Serie { get; set; }
        public string InvoiceNumber { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public string Active { get { return Status > 0 ? "Anulada" : "Activa"; } }
    }
}
