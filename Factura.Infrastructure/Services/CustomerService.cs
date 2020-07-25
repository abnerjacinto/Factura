using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repository;
        public CustomerService()
        {
            _repository = new CustomerRepository();
        }
        public void Create(Customer entity)
        {
            _repository.Create(entity);
        }

        public bool Delete(int id)
        {
            var _customer = _repository.FindbyId(id);
            if (_customer == null)
            {
                throw new Exception("Cliente no existe");
            }
            return _repository.Delete(id);
        }

        public Customer FindbyId(int id)
        {
            return _repository.FindbyId(id);
        }

        public List<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public void Update(Customer entity)
        {
            // Reglas de Negocio 
            var _customer = _repository.FindbyId(entity.Id);
            if (_customer == null)
            {
                throw new Exception("Cliente no existe");
            }
            _repository.Update(entity);
        }
    }
}
