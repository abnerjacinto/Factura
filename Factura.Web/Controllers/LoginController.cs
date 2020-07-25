using Factura.Core.Entities;
using Factura.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Factura.Web.Controllers
{
    public class LoginController : Controller
    {
        private UserService _userService;
        public LoginController()
        {
            _userService = new UserService();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Password")] User _user)
        {
            var user = _userService.FindbyUserName(_user.UserName);
            if (user == null)
            {
                //mensaje de error
            }
            else {
                if (user.Password!=_user.Password || user.UserName != _user.UserName) {
                    
                    return View(); ;
                }
            }

            return RedirectToAction("Index", "Inicio");
        }
    }
}