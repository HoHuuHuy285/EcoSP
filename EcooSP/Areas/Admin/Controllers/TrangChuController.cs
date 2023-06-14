using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;
namespace EcooSP.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            BookShopEntities db = new BookShopEntities();
            var dsDonHang = db.DonSaches.ToList();
            return View(dsDonHang);
        }
    }
}