using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;

namespace EcooSP.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham

        #region THÔNG TIN
        public ActionResult ChiTiet(string tensach, int? idloai)
        {
            mapSach map = new mapSach();
            var data = map.spTimKiem(tensach, idloai??0);
            ViewBag.tensach = tensach;
            ViewBag.idloai = idloai;
            return View(data);
        }

        public ActionResult ThemMoi()
        {
            return View(new Sach() { NgayNhap = DateTime.Now, GiaBan = 0, SoLuong = 0});
        } 
        [HttpPost]
        public ActionResult ThemMoi(Sach model)
        {

            if (string.IsNullOrEmpty(model.TenSach) == true)
            {
                ModelState.AddModelError("", "Tên Sách không được bỏ trống !!");
                return View();
            }

            try
            {
                BookShopEntities db = new BookShopEntities();
                db.Saches.Add(model);
                db.SaveChanges();
                return RedirectToAction("ChiTiet");
            }
            catch
            {
                ModelState.AddModelError("", "Lỗi Hệ Thống !!");
                return View();
            }
        }


        public ActionResult CapNhat(int id)
        {
            var map = new mapSach();
            var sanPham = map.ChiTietSach(id);
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult CapNhat(Sach model)
        {
            if (string.IsNullOrEmpty(model.TenSach) == true)
            {
                ModelState.AddModelError("", "Tên Sách không được bỏ trống !!");
                return View();
            }
            try
            {
                BookShopEntities db = new BookShopEntities();
                //1. Tìm đối tượng cũ 
                var sachUpdate = db.Saches.Find(model.ID);
                //2. Gán giá trị mới cho đối tượng cũ 
                sachUpdate.GioiThieu = model.GioiThieu;
                sachUpdate.ConHang = model.ConHang;
                sachUpdate.GiaBan = model.GiaBan;
                sachUpdate.NgayNhap = model.NgayNhap;
                sachUpdate.SoLuong = model.SoLuong;
                sachUpdate.TenSach = model.TenSach;
                //3. Lưu lại 
                db.SaveChanges();
                return RedirectToAction("ChiTiet");
            }
            catch
            {
                ModelState.AddModelError("", "Lỗi Hệ Thống !!");
                return View();
            }
        }

        public ActionResult Xoa(int id)
        {
            BookShopEntities db = new BookShopEntities();
            var sanPham = db.Saches.Find(id);
            db.Saches.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("ChiTiet");
        }

        public ActionResult Detail(int id)
        {
            BookShopEntities db = new BookShopEntities();
            var sanPham = db.Saches.Find(id);
            return View(sanPham);
        }
        #endregion


        #region PHAN LOAI
        public ActionResult ThemPhanLoai(int idSach)
        {
            return View(new PhanLoaiSach() { ID_Sach = idSach });
        }
        [HttpPost]
        public ActionResult ThemPhanLoai(PhanLoaiSach model)
        {
            // Check ID Sách == null
            // Check phân loại đã phân loại trước đây -> nếu có rồi thì thôi 

            BookShopEntities db = new BookShopEntities();
            var soluong = db.PhanLoaiSaches.Count(m => m.ID_Sach == model.ID_Sach && m.ID_TheLoai == model.ID_TheLoai);
            if (soluong > 0)
            {
                ModelState.AddModelError("", "ID Thể Loại đã được thêm trước đó");
                return View(model);
            }

            db.PhanLoaiSaches.Add(model);
            db.SaveChanges();
            return RedirectToAction("Detail",new { id = model.ID_Sach});
        }

        public ActionResult CapNhatPhanLoai(int id)
        {
            BookShopEntities db = new BookShopEntities();
            return View(db.PhanLoaiSaches.Find(id));
        }
        [HttpPost]
        public ActionResult CapNhatPhanLoai(PhanLoaiSach model)
        {
            // Check ID phân loại == null
            // Check phân loại đã phân loại trước đây -> nếu có rồi thì thôi 

            BookShopEntities db = new BookShopEntities();
            // Đếm dòng cùng sản phẩm , cùng phân loại sản phẩm ( Trừ chính nó ra ) 
            // sp 1 , loai 1 , phanloailan1 
            // sp1 , loai 2, phanloailan 2
            var updateModel = db.PhanLoaiSaches.Find(model.ID_PhanLoai);

            var soluong = db.PhanLoaiSaches.Count(m => m.ID_Sach == updateModel.ID_Sach & m.ID_TheLoai == model.ID_TheLoai & m.ID_PhanLoai != model.ID_PhanLoai);
            if (soluong > 0)
            {
                ModelState.AddModelError("", "ID Thể Loại đã được thêm trước đó");
                return View(model);
            }
            updateModel.ID_TheLoai = model.ID_TheLoai;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = updateModel.ID_Sach });
        }


        #endregion

        #region HÌNH ẢNH

        public ActionResult CapNhatHinhAnh(int idSanPham)
        {
            ViewBag.idSanPham = idSanPham;

            return View();
        }
        [HttpPost]
        public ActionResult CapNhatHinhAnh(int idSanPham, HttpPostedFileBase fileAnh)
        {
            //1. Tham số đầu vào kiểu HttpPostedFileBase
            //2. Kiểm tra file có dữ liệu hay không ? 
            if(fileAnh == null || fileAnh.ContentLength == 0)
            {
                ViewBag.error = "Chưa chọn file ";
                return View();
            }
            if (fileAnh.ContentLength == 0)
            {
                ViewBag.error = "File Không Có Nội Dung";
                return View();
            }

            // 3. Xác định đường dẫn lưu file : Url tương đối => Tuyệt đối
            var urlTuongDoi = "/Data/Image/"; // lấy đường dẫn lưu database
            var urlTuyetDoi = Server.MapPath(urlTuongDoi); // lấy đường dẫn lưu file trên sever 

            //4. Lưu file ( Chưa kiểm tra trùng file )
            //Check trùng tên file => Đổi tên file = tên file cũ ( không kèm đuôi ) + "-"+ số + đuôi
            // anh.jpg => anh + "-" + 1 + ".jpg" => anh-1.jpg
            //1. Tách tên và đuôi 
            //2. Sử dụng biến i để chạy và cộng vào tên file mới 
            //3. Check lại 
            string fullDuongDan = urlTuyetDoi + fileAnh.FileName;
            int i = 1;
            while(System.IO.File.Exists(fullDuongDan) == true )
            {
                string ten = Path.GetFileNameWithoutExtension(fileAnh.FileName);
                string duoi = Path.GetExtension(fileAnh.FileName);
                fullDuongDan = urlTuyetDoi + ten + '-' + i + duoi;
                i++;
            }
            fileAnh.SaveAs(fullDuongDan);  
            //5. Lưu Database 

            mapSach map = new mapSach();
            map.CapNhatHinhAnh(idSanPham, urlTuongDoi + Path.GetFileName(fullDuongDan));
            
            return RedirectToAction("Detail", new { id = idSanPham });
        }



        #endregion
    }
}



















