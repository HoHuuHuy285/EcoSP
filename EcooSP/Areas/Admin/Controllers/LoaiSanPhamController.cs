using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;

namespace EcooSP.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        public ActionResult ChiTiet(string tentheloai)
        {
            mapLoaiSanPham map = new mapLoaiSanPham();
            var data = map.DanhSach(tentheloai);
            ViewBag.tentheloai = tentheloai;
            return View(data);
        }


        public ActionResult ThemMoi()
        {
            return View(new TheLoai() { });
        }
        [HttpPost]
        public ActionResult ThemMoi(TheLoai model)
        {
            if(model.ID_TheLoai <= 0)
            {
                ModelState.AddModelError("", "ID Phải lớn hơn 0");
                return View();
            }    
            if (string.IsNullOrEmpty(model.TheLoai1) == true)
            {
                ModelState.AddModelError("", "Thể loại không được bỏ trống !!");
                return View();
            }    

            try
            {
                BookShopEntities db = new BookShopEntities();
                db.TheLoais.Add(model);
                db.SaveChanges();
                return RedirectToAction("ChiTiet");
            }
            catch
            {
                ModelState.AddModelError("", "Lỗi Hệ Thống !!");
                return View();
            }
        }
    }
}