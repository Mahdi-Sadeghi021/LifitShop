using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repositories.ProductRepsitory
{
    public interface IProductRepository
    {
        IQueryable<Products> GetAll(Expression<Func<Products, bool>> where = null);
        Task<Products> GetById(int Id);

        Task Add(Products Products);
        Task Update(Products Products);
        Task Delete(Products products);
        Task Delete(int Id);
    }
}
