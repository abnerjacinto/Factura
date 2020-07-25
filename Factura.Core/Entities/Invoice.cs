using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public DateTime InvoiceDate { get; set; }
        public int CustmerId { get; set; }
        public string Serie { get; set; }
        public string InvoiceNumber { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }
}
