using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderItemRepository
{
    public interface IOrderItemsRepository
    {
        IQueryable<OrderItem> GetAllBasketItem(Expression<Func<OrderItem, bool>> where = null);

        Task AddOrderItem(OrderItem item);

        Task DeleteOrderItem(OrderItem item);
    }
}
