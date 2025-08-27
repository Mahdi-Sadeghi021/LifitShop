using Business.ProductServise;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace LifitShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductServise _productServise;
        public ProductController(ProductServise productServise)
        {
            _productServise = productServise;
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productServise.GetProductsById(id);
            return View(product);
        }
    }
}
