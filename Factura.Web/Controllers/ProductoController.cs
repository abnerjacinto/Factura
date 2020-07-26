using Factura.Core.Entities;
using Factura.Infrastructure.Services;
using Factura.Web.Filters;
using Factura.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Factura.Web.Controllers
{
    [Autenticado]
    public class ProductoController : Controller
    {
        private ProductService _productService;
        public ProductoController()
        {
            _productService = new ProductService();
        }
        // GET: Producto
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = _productService.GetAll();
            return View(products);
        }
        public ActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Description,Unit,Price,Stock")] Product _product)
        {
            
            _productService.Create(_product);
            return RedirectToAction("Index", "Producto"); 
        }
        public ActionResult Actualizar(int id)
        {
            Product product = new Product();
            product = _productService.FindbyId(id);
            return View(product);
            
        }
        [HttpPost]
        public ActionResult Actualizar([Bind(Include = "Id,Name,Description,Unit,Price,Stock")] Product _product)
        {
            _productService.Update(_product);
            return RedirectToAction("Index", "Producto");            
        }
        public ActionResult Eliminar(int id)
        {
            Product product = new Product();
            product = _productService.FindbyId(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Eliminar([Bind(Include = "Id")] Product product)
        {
            _productService.Delete(product.Id);
            return RedirectToAction("Index", "Producto");
        }
    }
}