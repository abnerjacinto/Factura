using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public string NameDescription { get { return Name + " " + Description; } }
    }
}
