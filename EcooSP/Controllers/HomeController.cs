using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoSP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Mycart()
        {
            return View();
        }
        public ActionResult Whishlist()
        {
            return View();
        }
    }
}