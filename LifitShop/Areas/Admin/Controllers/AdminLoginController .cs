using LifitShop.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LifitShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly Dictionary<string, string> admins = new()
        {
            { "mohammad@ranjbar.com", "Pass123!" },
            { "amir@ranjbar.com", "Secret456@" },
            { "ali@barzegar.com", "Admin789#" }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (admins.TryGetValue(model.Email, out var correctPassword) &&
       correctPassword == model.Password)
            {
                // ست کردن Session برای ادمین
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("AdminEmail", model.Email);

                // بعد از ورود، مستقیم به صفحه لیست محصولات بره
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            ViewBag.Error = "ایمیل یا رمز عبور اشتباه است";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            HttpContext.Session.Remove("AdminEmail");
            return RedirectToAction("Login");
        }
    }
}
