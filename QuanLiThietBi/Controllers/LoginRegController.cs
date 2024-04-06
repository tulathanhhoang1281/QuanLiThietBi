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
        private readonly qlthietbiContext _db;


        public LoginRegController(qlthietbiContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IActionResult Index(string returnURL)
        {
            LoginVM loginModel = new LoginVM();
            loginModel.ReturnUrl = returnURL;
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM loginModel)
        {
            var user = _db.TblUsers.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);
            if (user != null && user.Status == 1)
            {
                var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = loginModel.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (!string.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                {
                    return Redirect(loginModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (user != null && user.Status == 0)
            {
                ViewBag.ErrorMessage = "<div class='error'>Tài khoản của bạn đã bị khóa.</div>";
                return View(loginModel);
            }
            else
            {
                ViewBag.ErrorMessage = "<div class='error'>Sai tên tài khoản hoặc mật khẩu</div>";
                return View(loginModel);
            }
        }   

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
     }
}
