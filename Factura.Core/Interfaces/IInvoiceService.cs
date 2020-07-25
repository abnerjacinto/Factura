using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface IInvoiceService
    {
        void Create(Invoice entity);
        void Update(Invoice entity);
        bool Delete(int id);
        Invoice FindbyId(int id);
        List<Invoice> GetAll();
        int GetLastId();
    }
}
