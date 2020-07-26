using Factura.Core.Entities;
using Factura.Infrastructure.Services;
using Factura.Web.Filters;
using Factura.Web.Helpers;
using Factura.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Factura.Web.Controllers
{
    [Autenticado]
    public class FacturaController : Controller
    {
        // GET: Factura
        private CustomerService _customerService;
        private ProductService _productService;
        private InvoiceService _invoiceService;
        private InvoiceDetailService _invoiceDetailService;
        Utils util = new Utils();
        public FacturaController()
        {
            _customerService = new CustomerService();
            _productService = new ProductService();
            _invoiceService = new InvoiceService();
            _invoiceDetailService = new InvoiceDetailService();
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
            invoiceView.Invoice.InvoiceDate = util.SetDate();
            Session["InvoiceView"] = invoiceView;
            return View(invoiceView);
        }
        public ActionResult BuscarCliente()
        {
            var list = _customerService.GetAll().ToList();
            ViewBag.CustomerId = new SelectList(list, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult BuscarCliente(InvoiceView invoiceView)
        {
            invoiceView = Session["InvoiceView"] as InvoiceView;
            var customerId = int.Parse(Request["CustomerId"]);
            invoiceView.Customer = _customerService.FindbyId(customerId);
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
            int qty = 0;
            invoiceView = Session["InvoiceView"] as InvoiceView;
            var productId = int.Parse(Request["ProductId"]);
            if (Request["ProductId"] != null)
            {
                qty = int.Parse(Request["Quantity"]);
            }

            var product = _productService.FindbyId(productId);
            if (product.Stock < qty)
            {
                ViewData["error"] = "No Existe Cantidad sufuciente para despachar";
                return View("NuevaFactura", invoiceView);
            }
            ProductView productView = new ProductView
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Unit = product.Unit,
                Price = product.Price,
                Quantity = qty
            };
            invoiceView.ProductList.Add(productView);
            var total = invoiceView.ProductList.Sum(x => x.Partial);
            invoiceView.Invoice.CustmerId = invoiceView.Customer.Id;
            invoiceView.Invoice.Total = total;
            invoiceView.Invoice.Tax = total * .12;
            var list = _productService.GetAll().ToList();
            ViewBag.ProductId = new SelectList(list, "Id", "NameDescription");
            return View("NuevaFactura", invoiceView);
        }
        [HttpPost]
        public ActionResult NuevaFactura(InvoiceView invoiceView)
        {
            var invoiceNumber = Request["Invoice.InvoiceNumber"];
            var serie = Request["Invoice.Serie"];
            invoiceView = Session["InvoiceView"] as InvoiceView;
            invoiceView.Invoice.InvoiceDate = util.SetDate();
            invoiceView.Invoice.CreatedTime = util.SetTime();
            invoiceView.Invoice.InvoiceNumber=invoiceNumber;
            invoiceView.Invoice.Serie = serie;
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
                var product = _productService.FindbyId(item.Id);
                product.Stock = product.Stock - item.Quantity;
                _productService.Update(product);
            }
            ViewData["success"] = "Factura agregada con exito";
            Session.Clear();
            return RedirectToAction("Index", "Factura");
        }
        public ActionResult AnularFactura(int id)
        {
            InvoiceView invoiceView = new InvoiceView();
            invoiceView.Invoice = new Invoice();
            invoiceView.Customer = new Customer();
            invoiceView.ProductList = new List<ProductView>();
            invoiceView.Invoice = _invoiceService.FindbyId(id);
            invoiceView.Customer = _customerService.FindbyId(invoiceView.Invoice.CustmerId);
            invoiceView.ProductList = (from product in _productService.GetAll()
                                       join invoiceDetail in _invoiceDetailService.GetAll() on product.Id equals invoiceDetail.ProductId
                                       where invoiceDetail.InvoiceId == id
                                       select new ProductView
                                       {
                                           Id = product.Id,
                                           Name = product.Name,
                                           Description = product.Description,
                                           Unit = product.Unit,
                                           Price = product.Price,
                                           Quantity = Convert.ToInt32(invoiceDetail.Qty),
                                       }).ToList();
            Session["InvoiceView"] = invoiceView;
            return View(invoiceView);
        }
        [HttpPost]
        public ActionResult AnularFactura(InvoiceView invoiceView)
        {
            
            
            invoiceView = Session["InvoiceView"] as InvoiceView;            
            
            invoiceView.Invoice.Status = 1; //Anulado
            invoiceView.Invoice.ModifiedTime = util.SetTime();
            invoiceView.Invoice.InvoiceDate = util.ConvertDate(invoiceView.Invoice.InvoiceDate,3);

            _invoiceService.Update(invoiceView.Invoice);
            foreach (ProductView item in invoiceView.ProductList) {
                var product = _productService.FindbyId(item.Id);
                product.Stock = product.Stock + item.Quantity;
                _productService.Update(product);
            }
            return RedirectToAction("Index", "Factura");
        }
        public ActionResult DetalleFactura(int id) {
            InvoiceView invoiceView = new InvoiceView();
            invoiceView.Invoice = new Invoice();
            invoiceView.Customer = new Customer();
            invoiceView.ProductList = new List<ProductView>();
            invoiceView.Invoice = _invoiceService.FindbyId(id);
            invoiceView.Customer = _customerService.FindbyId(invoiceView.Invoice.CustmerId);
            invoiceView.ProductList = (from product in _productService.GetAll()
                                       join invoiceDetail in _invoiceDetailService.GetAll() on product.Id equals invoiceDetail.ProductId
                                       where invoiceDetail.InvoiceId == id
                                       select new ProductView
                                       {
                                           Id = product.Id,
                                           Name = product.Name,
                                           Description = product.Description,
                                           Unit = product.Unit,
                                           Price = product.Price,
                                           Quantity = Convert.ToInt32(invoiceDetail.Qty),
                                       }).ToList();
            
            return View(invoiceView);
        }
    }
}