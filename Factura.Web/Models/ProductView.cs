using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factura.Web.Models
{
    public class ProductView:Product
    {
        public int Quantity { get; set; }
        public double Partial { get { return Price * Quantity; } }

    }
}