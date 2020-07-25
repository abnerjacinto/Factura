using Factura.Core.Entities;
using Factura.Infrastructure.Services;
using Factura.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Factura.Web.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura
        private CustomerService _customerService;
        private ProductService _productService;
        private InvoiceService _invoiceService;
        private InvoiceDetailService _invoiceDetailService;
        public FacturaController()
        {
            _customerService = new CustomerService();
            _productService = new ProductService();
            _invoiceService = new InvoiceService();
        }
        public ActionResult Index()
        {
            List<Invoice> invoices = new List<Invoice>();
            invoices = _invoiceService.GetAll();
            return View(invoices);
        }
        public ActionResult NuevaFactura()
        {
            InvoiceView invoiceView = new InvoiceView();
            invoiceView.Invoice = new Invoice();
            invoiceView.Customer = new Customer();
            invoiceView.ProductList = new List<ProductView>();
            invoiceView.Invoice.InvoiceDate = DateTime.Today;
            Session["InvoiceView"] = invoiceView;
            return View(invoiceView);
        }
        public ActionResult BuscarCliente() {
            var list = _customerService.GetAll().ToList();
            ViewBag.CustomerId = new SelectList(list, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult BuscarCliente(InvoiceView invoiceView)
        {
            invoiceView = Session["InvoiceView"] as InvoiceView;
            var customerId = int.Parse(Request["CustomerId"]);
            invoiceView.Customer=_customerService.FindbyId(customerId);
            var list = _customerService.GetAll().ToList();
            ViewBag.CustomerId = new SelectList(list, "Id", "FullName");
            return View("NuevaFactura", invoiceView);
        }
        public ActionResult BuscarProducto()
        {
            var list = _productService.GetAll().ToList();
            ViewBag.ProductId = new SelectList(list, "Id", "NameDescription");
            return View();
        }
        [HttpPost]
        public ActionResult BuscarProducto(InvoiceView invoiceView)
        {
            invoiceView = Session["InvoiceView"] as InvoiceView;
            var productId = int.Parse(Request["ProductId"]);
            var qty = int.Parse(Request["Quantity"]);
            var product = _productService.FindbyId(productId);
            if (product.Stock < qty)
            {
                TempData["msg"] = "<script>alert('No existe cantidad suficiente para despachar.');</script>";
                return View("NuevaFactura", invoiceView);
            }
            ProductView productView = new ProductView {
                Name = product.Name,
                Description = product.Description,
                Unit = product.Unit,
                Price = product.Price,
                Quantity = qty
            };
            invoiceView.ProductList.Add(productView);
            var total = invoiceView.ProductList.Sum(x => x.Partial);
            invoiceView.Invoice.Total=total;
            invoiceView.Invoice.Tax = total * .12;
            var list = _productService.GetAll().ToList();
            ViewBag.ProductId = new SelectList(list, "Id", "NameDescription");
            return View("NuevaFactura", invoiceView);
        }
        [HttpPost]
        public ActionResult NuevaFactura(InvoiceView invoiceView)
        {
            invoiceView = Session["InvoiceView"] as InvoiceView;
            _invoiceService.Create(invoiceView.Invoice);
            var idInvoice = _invoiceService.GetLastId();
            
            foreach (ProductView item in invoiceView.ProductList)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = idInvoice,
                    ProductId = item.Id,
                    Price = item.Price,
                    Qty = item.Quantity

                };
                _invoiceDetailService.Create(invoiceDetail);
            }
                return View();
        }
    }
}