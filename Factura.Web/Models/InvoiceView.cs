using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factura.Web.Models
{
    public class InvoiceView
    {
        public Invoice Invoice { get; set; }
        public Customer Customer { get; set; }
        public List<ProductView> ProductList { get; set; }
    }
}