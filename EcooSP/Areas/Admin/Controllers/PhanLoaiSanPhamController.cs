using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;

namespace EcooSP.Areas.Admin.Controllers
{
    public class PhanLoaiSanPhamController : Controller
    {
        // GET: Admin/PhanLoaiSanPham
        public ActionResult ChiTiet( )
        {
            mapPhanLoaiSanPham map = new mapPhanLoaiSanPham();
            var data = map.DanhSach();
            return View(data);
        }
    }
}