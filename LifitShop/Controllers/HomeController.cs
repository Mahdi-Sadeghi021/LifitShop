using System.Diagnostics;
using Business.ProductServise;
using LifitShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifitShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductServise _productServise;

        public HomeController(ILogger<HomeController> logger, ProductServise productServise)
        {
            _productServise = productServise;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _productServise.GetProductwithcategorybrand();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
