using Factura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Core.Interfaces
{
    public interface IUserService
    {
        void Create(User entity);
        void Update(User entity);
        bool Delete(int id);
        User FindbyId(int id);
        List<User> GetAll();
        int GetLastId();
        User FindbyUserName(string userName);
    }
}
