using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceRepository _repository;
        public InvoiceService()
        {
            _repository = new InvoiceRepository();
        }
        public void Create(Invoice entity)
        {
            _repository.Create(entity);
        }

        public bool Delete(int id)
        {
            var _invoice = _repository.FindbyId(id);
            if (_invoice == null)
            {
                throw new Exception("Factura no existe");
            }
            return _repository.Delete(id);
        }

        public Invoice FindbyId(int id)
        {
            return _repository.FindbyId(id);
        }

        public List<Invoice> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public void Update(Invoice entity)
        {
            // Reglas de Negocio 
            var _invoice = _repository.FindbyId(entity.Id);
            if (_invoice == null)
            {
                throw new Exception("Factura no existe");
            }
            _repository.Update(entity);
        }
    }
}
