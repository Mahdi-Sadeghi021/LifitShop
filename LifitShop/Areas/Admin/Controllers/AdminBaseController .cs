using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LifitShop.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        [Area("Admin")]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // چک کردن Session ادمین
            var adminEmail = context.HttpContext.Session.GetString("AdminEmail");

            if (string.IsNullOrEmpty(adminEmail))
            {
                // اگه ادمین نبود، ریدایرکت به صفحه لاگین
                context.Result = new RedirectToActionResult(
                    "Login",         // اکشن
                    "AdminLogin",    // کنترلر لاگین
                    new { area = "Admin" }
                );
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

