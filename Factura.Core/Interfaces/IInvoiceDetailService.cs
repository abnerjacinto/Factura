using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface IInvoiceDetailService
    {
        void Create(InvoiceDetail entity);
        void Update(InvoiceDetail entity);
        bool Delete(int id);
        InvoiceDetail FindbyId(int id);
        List<InvoiceDetail> GetAll();
        int GetLastId();
    }
}
