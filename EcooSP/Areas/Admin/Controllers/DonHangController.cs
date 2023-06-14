using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;
namespace EcooSP.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        public ActionResult ChiTiet(string tenkhachhang)
        {
            mapDonHang map = new mapDonHang();  
            var data = map.DanhSach(tenkhachhang);
            ViewBag.tenkhachhang = tenkhachhang;
            return View(data);
        }
    }
}