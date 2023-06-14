using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcooSP.Models
{
    public class mapSach
    {
        public List<Sach> DanhSach()
        {
            var db = new BookShopEntities();


            var data = db.Saches.ToList();
            return data;
        }

        public List<Sach> TimKiem(string tensach)
        {
            var db = new BookShopEntities();


            var data = db.Saches.Where(m => m.TenSach.ToLower().Contains(tensach.ToLower()) == true || string.IsNullOrEmpty(tensach));

            //3. Sắp xếp kết quả đầu ra 

            return data.OrderBy(m => m.TenSach).ToList();
        }

        public List<Sach> ChiTiet(string tensach, int? idloai)
        {
            var db = new BookShopEntities();


            var data = (from item in db.Saches
                        where(item.TenSach.ToLower().Contains(tensach.ToLower()) == true || string.IsNullOrEmpty(tensach))
                        && (idloai == null)  // || item.ID_TheLoai == idloai)
                        select item
                        ).ToList();

            //3. Sắp xếp kết quả đầu ra 

            return data.OrderBy(m => m.TenSach).ToList();
        }


        public Sach ChiTietSach(int id)
        {
            BookShopEntities db = new BookShopEntities();
            return db.Saches.Find(id); // Chỉ tìm kiếm khoá chính 
            // where ( m=>m.idSanPham = id ) 
        }



        public List<spDanhSachSanPham_Result> spTimKiem(string tenSach, int idTheLoai)
        {
            BookShopEntities db = new BookShopEntities();
            return db.spDanhSachSanPham(tenSach, idTheLoai).ToList();
        }

        public bool CapNhatHinhAnh(int idSanPham, string urlHinhAnh)
        {
            try
            {
                BookShopEntities db = new BookShopEntities();
                var sanPham = db.Saches.Find(idSanPham);
                sanPham.HinhAnh = urlHinhAnh;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }   
}