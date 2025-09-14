using System.Diagnostics;
using Business.ProductServise;
using DataAccess.Models;
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
        public async Task<IActionResult> AboutUs()

        {
            return View();
        }
        public async Task<IActionResult> Article()
        {
            return View();
        }
        //public async Task<IActionResult> Store(int page = 1, int PageSize = 6, string search = null, int? categoryId = null)
        //{
        //    var data = await _productServise.GetProductPageInation(page, PageSize, search);
        //    ViewBag.search = search;
        //    return View(data);
        //}
        //public async Task<IActionResult> Store(int page = 1, int PageSize = 6, string search = null, int? categoryId = null)
        //{
        //    var data = await _productServise.GetProductPageInation(page, PageSize, search, categoryId);
        //    ViewBag.search = search;
        //    ViewBag.CategoryId = categoryId; // برای اینکه تو ویو بدونی الان روی چه دسته‌ای هستی
        //    return View(data);
        //}

        public async Task<IActionResult> Store(int page = 1, int PageSize = 6, string search = null, int? categoryId = null, int? brandId = null)
        {
            var data = await _productServise.GetProductPageInation(page, PageSize, search, categoryId, brandId);
            ViewBag.search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.BrandId = brandId;
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


        [HttpGet]
        public async Task<IActionResult> SearchHeader(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var products = await _productServise.SearchProductsAsync(term); // لیستی از محصولاتی که term تو نامش هست
            var result = products.Select(p => new {
                id = p.ProductId,
                name = p.ProductTitle,
                image = p.ImageName, // مسیر عکس
                price = p.ProductPrice
            });

            return Json(result);
        }
    }
}
