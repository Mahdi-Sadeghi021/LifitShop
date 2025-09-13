using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repositories.OrderRepository;
using DataAccess.Repositories.ProductRepsitory;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.OrderService
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository OrderRepository, IProductRepository ProductRepository)
        {
            _orderRepository = OrderRepository;
            _productRepository = ProductRepository;
        }

        public async Task<bool> AddToBasket(int productId, int Qty, int userId)
        {
            var basket = new Order();
            basket = await _orderRepository.GetAll(a => a.UserId == userId && a.Status == DataAccess.Enums.Status.Create)
                .FirstOrDefaultAsync();

            if (basket == null)
            {
                basket = new Order()
                {
                    UserId = userId,
                    Created = DateTime.Now,
                    Status = DataAccess.Enums.Status.Create,
                    Address = "",
                    Mobile = "",
                    PostalCode="",
                    Payed = DateTime.Now,
                };
                await _orderRepository.Add(basket);
            }

            var products = await _productRepository.GetById(productId);

            var BasketItem = new OrderItem()
            {
                OrderId = basket.Id,
                Quantity = Qty,
                ProductId =products .ProductId,
                Created = DateTime.Now,
                TotalPrice = products.ProductPrice * Qty,
            };

            await _orderRepository.AddOrdertItem(BasketItem);

            return true;
        }


        public async Task<List<OrderItem>> GetUserBasket(int userId)
        {
            var orders = await _orderRepository.GetAllOrdertItems(a => a.order.UserId == userId
             && a.order.Status == DataAccess.Enums.Status.Create)
                .Include(a => a.order)
                .Include(a => a.Product).AsNoTracking().ToListAsync();
            return orders;
        }

    }
}
