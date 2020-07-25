using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface IProductService
    {
        void Create(Product entity);
        void Update(Product entity);
        bool Delete(int id);
        Product FindbyId(int id);
        List<Product> GetAll();
        int GetLastId();
    }
}
