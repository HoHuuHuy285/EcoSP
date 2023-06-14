using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcooSP.Models
{
    public class mapLoaiSanPham
    {
        public List<TheLoai> DanhSach()
        {
            var db = new BookShopEntities();


            var data = db.TheLoais.ToList();
            return data;
        }

        public List<TheLoai> DanhSach(string diaChiTimKiem)
        {
            var db = new BookShopEntities();


            var data = db.TheLoais.Where(m => m.TheLoai1.ToLower().Contains(diaChiTimKiem.ToLower()) == true || string.IsNullOrEmpty(diaChiTimKiem)).ToList();

            //3. Sắp xếp kết quả đầu ra 

            return data.OrderBy(m => m.TheLoai1).ToList();
        }

        public List<PhanLoaiSach> DanhSachTheoSanPham(int idSach)
        {
            var db = new BookShopEntities();


            var data = (from item in db.PhanLoaiSaches
                        join loai in db.TheLoais on item.ID_TheLoai equals loai.ID_TheLoai
                        where item.ID_Sach == idSach
                        select item     
                        ).ToList(); 
            return data;
        }

    }
}