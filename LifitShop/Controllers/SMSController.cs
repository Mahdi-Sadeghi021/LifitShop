using Business.SMSService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;

namespace LifitShop.Controllers
{
    public class SMSController : Controller
    {
        public readonly ISMSService _smsService;
        public SMSController(ISMSService smsService)
        {
            _smsService = smsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendPublicSMS()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> SendLookupSMS()
        {
            await _smsService.SendLookupSMS(phoneNumber:"09377840848", "ContactUsVerification", token1:"مهدی", token2:Generator.RandomNumber());

            return Redirect("/");
        }
    }
}
