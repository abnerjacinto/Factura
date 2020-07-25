using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Entities
{
    public class InvoiceDetail:BaseEntity
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }

    }
}
