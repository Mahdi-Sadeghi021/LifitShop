using DataAccess.Data;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly LifitDbContext _context;

        public OrderRepository(LifitDbContext context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrdertItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var Order = await GetById(Id);
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrderItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll(Expression<Func<Order, bool>> where = null)
        {
            var orders = _context.Orders.AsQueryable();
            if (where != null)
            {
                orders = orders.Where(where);
            }
            return orders;
        }

        public IQueryable<OrderItem> GetAllOrdertItems(Expression<Func<OrderItem, bool>> where = null)
        {
            var baskets = _context.OrderItems.AsQueryable();
            if (where != null)
            {
                baskets = baskets.Where(where);
            }

            return baskets;
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


    }
}

