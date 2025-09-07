using Business.SMSService;
using Business.UserService;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LifitShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISMSService _smsService;
        private readonly IMemoryCache _cache;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IUserService userService,
            ISMSService smsService,
            IMemoryCache cache,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _smsService = smsService;
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                // نمایش همه خطاهای مدل
                return View(model);
            }

            try
            {
                // بررسی اینکه کاربر با این شماره موبایل قبلاً وجود ندارد
                var existingUser = await _userManager.FindByNameAsync(model.mobileNumber);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "این شماره موبایل قبلاً ثبت‌نام شده است.");
                    return View(model);
                }

                // ایجاد کاربر جدید
                var user = new User
                {
                    UserName = model.mobileNumber,
                    mobileNumber = model.mobileNumber,
                    FullName = model.FullName,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };

                // ثبت کاربر در Identity
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // ورود کاربر بعد از ثبت‌نام
                await _signInManager.SignInAsync(user, isPersistent: true);

                // هدایت به صفحه اصلی
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            }


            /// <summary>
            /// نمایش فرم ورود (شماره موبایل)
            /// </summary>
            [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        /// <summary>
        /// دریافت شماره موبایل و ارسال کد OTP
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SendCode(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                ModelState.AddModelError("", "شماره موبایل الزامی است");
                return View("Login");
            }

            // تولید کد
            var code = Generator.RandomNumber();

            // ذخیره موقت در Cache برای 2 دقیقه
            _cache.Set($"otp_{phoneNumber}", code, TimeSpan.FromMinutes(2));

            // ارسال پیامک
            await _smsService.SendLookupSMS(phoneNumber, "LoginTemplate", code);

            TempData["PhoneNumber"] = phoneNumber;
            return RedirectToAction("VerifyCode");
        }

        /// <summary>
        /// نمایش فرم وارد کردن کد
        /// </summary>
        [HttpGet]
        public IActionResult VerifyCode()
        {
            return View();
        }

        /// <summary>
        /// بررسی کد وارد شده
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> VerifyCode(string phoneNumber, string code)
        {
            if (!_cache.TryGetValue($"otp_{phoneNumber}", out string savedCode) || savedCode != code)
            {
                ModelState.AddModelError("", "کد وارد شده صحیح نیست یا منقضی شده است.");
                return View();
            }

            // کاربر موجود است؟
            var user = _userService.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                // ثبت‌نام سریع
                user = new User
                {
                    UserName = phoneNumber,
                    mobileNumber = phoneNumber,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "خطا در ثبت‌نام کاربر.");
                    return View();
                }
            }

            // ورود
            await _signInManager.SignInAsync(user, isPersistent: true);

            // پاک کردن OTP
            _cache.Remove($"otp_{phoneNumber}");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// خروج کاربر
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
