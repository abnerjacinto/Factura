using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        bool Delete(int id);
        T FindbyId(int id);
        List<T> GetAll();
        int GetLastId();
    }
}
