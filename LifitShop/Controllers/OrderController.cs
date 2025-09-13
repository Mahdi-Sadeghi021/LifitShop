using System.Security.Claims;
using Business.OrderService;
using LifitShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifitShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddToOrder([FromBody] AddOrderDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Ok(new { res = false, msg = "شما لاگین نکرده اید" });
            }

            var result = await _orderService .AddToBasket(model.ProductId, model.qty, Convert.ToInt32(userId));
            return Ok(new { res = true });
        }


        [Authorize]
        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _orderService.GetUserBasket(Convert.ToInt32(userId));
            return View(data);
        }

    }
}
