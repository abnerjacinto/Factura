using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;
        public ProductService()
        {
            _repository = new ProductRepository();
        }
        public void Create(Product entity)
        {
            _repository.Create(entity);
        }

        public bool Delete(int id)
        {
            var _product = _repository.FindbyId(id);
            if (_product == null)
            {
                throw new Exception("Registro no existe");
            }
            return _repository.Delete(id);
        }

        public Product FindbyId(int id)
        {
            return _repository.FindbyId(id);
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public void Update(Product entity)
        {
            // Reglas de Negocio 
            var _product = _repository.FindbyId(entity.Id);
            if (_product == null)
            {
                throw new Exception("Registro no existe");
            }
            _repository.Update(entity);
        }
    }
}
