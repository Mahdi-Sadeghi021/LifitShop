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
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // بررسی اینکه شماره موبایل قبلاً در Cache هست یا نه
            if (_cache.TryGetValue($"otp_pending_{model.mobileNumber}", out _))
            {
                ModelState.AddModelError("", "برای این شماره موبایل کد تایید ارسال شده است. لطفا کد را وارد کنید.");
                return View(model);
            }

            // تولید کد OTP
            var code = Generator.RandomNumber();

            // ذخیره اطلاعات فرم و OTP در Cache برای 5 دقیقه
            _cache.Set($"otp_pending_{model.mobileNumber}", new
            {
                UserData = model,
                OTP = code
            }, TimeSpan.FromMinutes(5));

            // ارسال پیامک (اگر سرویس تستی باشه، فقط به شماره خود سرویس ارسال می‌شود)
            _smsService.SendLookupSMS(model.mobileNumber, "RegisterTemplate", code);

            TempData["PhoneNumber"] = model.mobileNumber;
            return RedirectToAction("VerifyCode");
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
            if (!_cache.TryGetValue($"otp_pending_{phoneNumber}", out dynamic pending))
            {
                ModelState.AddModelError("", "کد منقضی شده است یا شماره موبایل معتبر نیست.");
                return View();
            }

            if (pending.OTP != code)
            {
                ModelState.AddModelError("", "کد وارد شده صحیح نیست.");
                return View();
            }

            // بررسی اینکه کاربر با این شماره موبایل قبلاً ثبت نشده باشد
            var existingUser = await _userManager.FindByNameAsync(phoneNumber);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "این شماره موبایل قبلاً ثبت‌نام شده است.");
                return View();
            }

            // ساخت کاربر با تمام اطلاعات فرم
            var model = pending.UserData as User;
            var user = new User
            {
                UserName = model.mobileNumber,
                mobileNumber = model.mobileNumber,
                FullName = model.FullName,
                Address = model.Address,
                PostalCode = model.PostalCode,
                Email = model.Email,
                RegisterDate = DateTime.Now,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "خطا در ثبت‌نام کاربر.");
                return View();
            }

            // ورود کاربر
            await _signInManager.SignInAsync(user, isPersistent: true);

            // پاک کردن Cache
            _cache.Remove($"otp_pending_{phoneNumber}");

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
