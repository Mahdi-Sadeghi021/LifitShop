using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class User: IdentityUser<int>
    {
        // نام و نام خانوادگی
        public string? FullName { get; set; }

        //شماره موبایل
        public string PhoneNumber { get; set; }

        // آدرس برای ارسال سفارش
        public string? Address { get; set; }

        // کد پستی
        public string? PostalCode { get; set; }

        // شماره ملی (اختیاری – برای احراز هویت قوی‌تر)
        public string? NationalCode { get; set; }

        // تاریخ تولد (برای پیشنهادات و تخفیف‌ها)
        public DateTime? BirthDate { get; set; }

        // تاریخ عضویت (برای گزارش‌گیری و امتیاز مشتری)
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        // وضعیت حساب (فعال یا غیر فعال)
        public bool IsActive { get; set; } = true;
    }
}
