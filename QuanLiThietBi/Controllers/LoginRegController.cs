using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Models;
using SQLitePCL;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace QuanLiThietBi.Controllers
{
    public class LoginRegController : Controller
    { 
        private readonly UserManager<TblUser> _userManager;
        private readonly SignInManager<TblUser> _signInManager;
        private readonly qlthietbiContext db;


        public LoginRegController(qlthietbiContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            var user = db.TblUsers.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Lưu thông tin đăng nhập (session, cookie,...)
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Đăng nhập thất bại!";
            return View("Index");
        }   

        [HttpPost]
        public IActionResult Logout()
        {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
        }
     }
}
