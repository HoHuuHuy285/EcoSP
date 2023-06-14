using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoSP.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult ChiTiet()
        {
            return View();
        }

        public ActionResult TheLoai()
        {
            return View();
        }
    }
}