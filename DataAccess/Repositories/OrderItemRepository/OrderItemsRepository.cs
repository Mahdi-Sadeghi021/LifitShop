using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderItemRepository
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly LifitDbContext _context;

        public OrderItemsRepository(LifitDbContext context)
        {
            _context = context;
        }

        public IQueryable<OrderItem> GetAllBasketItem(Expression<Func<OrderItem, bool>> where = null)
        {
            var orders = _context.OrderItems.AsQueryable();
            if (where != null)
            {
                orders = orders.Where(where);
            }
            return orders;
        }

        public async Task AddOrderItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

       
    }
}
