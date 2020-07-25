using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface ICustomerService
    {
        void Create(Customer entity);
        void Update(Customer entity);
        bool Delete(int id);
        Customer FindbyId(int id);
        List<Customer> GetAll();
        int GetLastId();
    }
}
