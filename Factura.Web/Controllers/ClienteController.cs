using Factura.Core.Entities;
using Factura.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Factura.Web.Controllers
{
    public class ClienteController : Controller
    {
        private CustomerService _customerService;
        public ClienteController()
        {
            _customerService = new CustomerService();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            customers = _customerService.GetAll();
            return View(customers);
        }
        public ActionResult Nuevo()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo([Bind(Include = "FirstName,LastName,NIT,Address,Phone,Email")] Customer _customer)
        {
            _customerService.Create(_customer);
            return RedirectToAction("Index", "Cliente");
        }
        public ActionResult Actualizar(int id)
        {
            Customer customer = new Customer();
            customer = _customerService.FindbyId(id);
            return View(customer);

        }
        [HttpPost]
        public ActionResult Actualizar([Bind(Include = "Id,FirstName,LastName,NIT,Address,Phone,Email")] Customer _customer)
        {
            _customerService.Update(_customer);
            return RedirectToAction("Index", "Cliente");
        }
        public ActionResult Eliminar(int id)
        {
            Customer customer = new Customer();
            customer = _customerService.FindbyId(id);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Eliminar([Bind(Include = "Id")] Product product)
        {
            _customerService.Delete(product.Id);
            return RedirectToAction("Index", "Cliente");
        }
    }
}