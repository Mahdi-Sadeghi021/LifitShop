using Business.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace LifitShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string phoneNumber)
        {
            // 1) چک کن توی دیتابیس هست یا نه
            var user = _userService.GetUserByPhoneNumber(phoneNumber);

            if (user != null)
            {
                // اگه کاربر قبلا ثبت‌نام کرده → مستقیم لاگین بشه
                // (اینجا می‌تونی کوکی / سشن ست کنی یا بفرستیش به صفحه داشبورد)
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // اگه نبود → بفرست به صفحه ثبت‌نام
                return RedirectToAction("Register", "Account", new { phoneNumber = phoneNumber });
            }
        }
    }
}
