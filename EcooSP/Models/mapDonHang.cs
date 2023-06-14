using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcooSP.Models
{
    public class mapDonHang
    {
        public List<DonSach> DanhSach()
        {
            var db = new BookShopEntities();


            var data = db.DonSaches.ToList();
            return data;
        }

        public List<DonSach> DanhSach(string diaChiTimKiem)
        {
            var db = new BookShopEntities();


            var data = db.DonSaches.Where(m => m.TenKhachHang.ToLower().Contains(diaChiTimKiem.ToLower()) == true || string.IsNullOrEmpty(diaChiTimKiem));

            //3. Sắp xếp kết quả đầu ra 

            return data.OrderBy(m => m.TenKhachHang).ToList();
        }
    }
}