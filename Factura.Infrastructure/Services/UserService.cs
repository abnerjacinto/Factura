using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }
        public void Create(User entity)
        {
            _repository.Create(entity);
        }

        public bool Delete(int id)
        {
            var _user = _repository.FindbyId(id);
            if (_user == null)
            {
                throw new Exception("Usuario no existe");
            }
            return _repository.Delete(id);
        }

        public User FindbyId(int id)
        {
            return _repository.FindbyId(id);
        }

        public User FindbyUserName(string userName)
        {
            return _repository.FindbyUserName(userName);
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public void Update(User entity)
        {
            // Reglas de Negocio 
            var _user = _repository.FindbyId(entity.Id);
            if (_user == null)
            {
                throw new Exception("Registro no existe");
            }
            _repository.Update(entity);
        }
    }
}
