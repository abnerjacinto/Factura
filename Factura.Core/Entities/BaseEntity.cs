﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public double CreatedTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public double ModifiedTime { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int ModifiedUserId { get; set; }
    }
}
