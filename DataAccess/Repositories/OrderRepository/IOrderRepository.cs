using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll(Expression<Func<Order, bool>> where = null);
        Task<Order> GetById(int Id);

        Task Add(Order order);
        Task Update(Order order);
        Task Delete(Order order);
        Task Delete(int Id);
    }
}
