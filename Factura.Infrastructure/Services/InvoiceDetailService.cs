using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly InvoiceDetailRepository _repository;
        public InvoiceDetailService()
        {
            _repository = new InvoiceDetailRepository();
        }
        public void Create(InvoiceDetail entity)
        {
            _repository.Create(entity);
        }

        public bool Delete(int id)
        {
            var _InvoiceDetail = _repository.FindbyId(id);
            if (_InvoiceDetail == null)
            {
                throw new Exception("Registro no existe");
            }
            return _repository.Delete(id);
        }

        public InvoiceDetail FindbyId(int id)
        {
            return _repository.FindbyId(id);
        }

        public List<InvoiceDetail> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public void Update(InvoiceDetail entity)
        {
            // Reglas de Negocio 
            var _invoiceDetail = _repository.FindbyId(entity.Id);
            if (_invoiceDetail == null)
            {
                throw new Exception("Registro no existe");
            }
            _repository.Update(entity);
        }
    }
}
