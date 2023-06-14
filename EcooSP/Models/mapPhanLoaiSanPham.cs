using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcooSP.Models
{
    public class mapPhanLoaiSanPham
    {
        public List<PhanLoaiSach> DanhSach()
        {
            BookShopEntities db = new BookShopEntities();
            var data = db.PhanLoaiSaches.ToList();
            return data;
        }

    }

}