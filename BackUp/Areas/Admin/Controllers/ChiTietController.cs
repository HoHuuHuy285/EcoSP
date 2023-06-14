using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;

namespace EcooSP.Areas.Admin.Controllers
{
    public class ChiTietController : Controller
    {
        // GET: Admin/ChiTiet
        public ActionResult Sach(  string TimKiem, int? S_DanhMucId)
        {
            mapSach map = new mapSach();
            var data = map.spTimKiem(TimKiem, S_DanhMucId??0);
            ViewBag.TimKiem = TimKiem; 
            ViewBag.S_DanhMucId = S_DanhMucId;
            return View(data);
        }
        public ActionResult NhaXuatBan(string diachi)
        {
            mapNhaXuatBan map = new mapNhaXuatBan();
            var data = map.DanhSachNhaXuatBan(diachi);
            ViewBag.diachi = diachi;
            return View(data);
        }
        public ActionResult DanhMucSach(string diachi)
        {
            mapDanhMucSach map = new mapDanhMucSach();
            var data = map.DanhSachDanhMuc(diachi);
            ViewBag.diachi = diachi;
            return View(data);
        }
        public ActionResult TacGia(string diachi)
        {
            mapTacGia map = new mapTacGia();
            var data = map.DanhSachTacGia(diachi);
            ViewBag.diachi = diachi;
            return View(data);
        }

        // 1. View tạo form 
        public ActionResult ThemMoi()
        {
            return View( new NHAXUATBAN() { } );
        }
        // 2. Action lưu dữ liệu 
        [HttpPost]
        public ActionResult ThemMoi(NHAXUATBAN model)
        {
            // Thực hiện lưu 
            //1. Check data 
            if(model.NXB_ID <= 0)
            {
                //Thêm báo lỗi
                ModelState.AddModelError("", "ID phải lớn hơn 0");
                return View(model);
            }
            if (string.IsNullOrEmpty( model.NXB_Ten) == true)
            {
                //Thêm báo lỗi
                ModelState.AddModelError("", "Bạn chưa nhập tên");
                return View(model);
            }
            //2. Lưu Data 
            try
            {
                BookEcoEntities1 db = new BookEcoEntities1();
                db.NHAXUATBANs.Add(model);
                db.SaveChanges();
                return RedirectToAction("NhaXuatBan");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ Thống Lỗi");
                return View();
            }
            //3. Chuyển Hướng link 
        }

        public ActionResult ThemMoiDMSach()
        {
            return View(new DANHMUCSACH() { });
        }
        [HttpPost]
        public ActionResult ThemMoiDMSach(DANHMUCSACH model)
        {
            if(model.DMS_Id <= 0)
            {
                ModelState.AddModelError("", "ID phải lớn hơn 0");
                return View(model);
            }
            try
            {
                BookEcoEntities1 db = new BookEcoEntities1();
                db.DANHMUCSACHes.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhMucSach");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ Thống Lỗi");
                return View();
            }
        }

        public ActionResult ThemMoiTacGia()
        {
            return View(new TACGIA() { });
        }
        [HttpPost]
        public ActionResult ThemMoiTacGia(TACGIA model)
        {
            if(model.TG_Id <= 0)
            {
                ModelState.AddModelError("", "ID phải lớn hơn 0");
                return View(model);
            }
            if (string.IsNullOrEmpty (model.TG_HoTen) == true )
            {
                ModelState.AddModelError("", "Họ và tên không được để trống ");
                return View(model);
            }
            try
            {
                BookEcoEntities1 db = new BookEcoEntities1();
                db.TACGIAs.Add(model);
                db.SaveChanges();
                return RedirectToAction("TacGia");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ Thống Lỗi");
                return View();
            }
        }


        public ActionResult ChiTietSach(int id)
        {
            BookEcoEntities1 db = new BookEcoEntities1();
            var sanPham = db.SACHes.Find(id);
            return View(sanPham);
        }

        #region PHAN LOAI
        public ActionResult ThemPhanLoai(int idSanPham)
        {
            return View(new )
        }
        #endregion
    }
}