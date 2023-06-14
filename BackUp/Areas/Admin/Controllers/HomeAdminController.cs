using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcooSP.Models;

namespace EcooSP.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        // Đăng nhập 

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) == true || string.IsNullOrEmpty(password) == true)
            {
                ViewBag.error = " Thiếu thông tin đăng nhập";
                return View();
            }
            var db = new BookEcoEntities1();
            // Lấy tài khoản
            var user = db.QUANLies.SingleOrDefault(m => username.ToLower() == username.ToLower());
            // Kiếm tra tài khoản có tồn tại hai không 
            if (user == null)
            {
                ViewBag.error = " Tài khoản không tồn tại ";
                ViewBag.username = username;
                return View();
            }
            // Kiểm tra mật khẩu 
            if (user.QL_MatKhau != password)
            {
                ViewBag.error = "Mật Khẩu Không Đúng!! ";
                return View();
            }

            // Chắc chắn đúng : Lưu session 
            Session["user"] = user;
            return RedirectToAction("Index");
        }
    }
}