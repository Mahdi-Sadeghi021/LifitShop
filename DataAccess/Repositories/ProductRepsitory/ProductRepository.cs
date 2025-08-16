using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.ProductRepsitory
{
    public class ProductRepository : IProductRepository
    {
        private readonly LifitDbContext _context;

        public ProductRepository(LifitDbContext context)
        {
            _context = context;
        }

        public async Task Add(Products Products)
        {
            _context.Products.Add(Products);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(Products Products)
        {
            _context.Products.Remove(Products);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var Products = await GetById(Id);
            _context.Products.Remove(Products);
            await _context.SaveChangesAsync();
        }



        public IQueryable<Products> GetAll(Expression<Func<Products, bool>> where = null)
        {
            var Products = _context.Products.Include(p => p.Brand)       // بارگذاری برند
        .Include(p => p.Category).AsQueryable();
            if (where != null)
            {
                Products = Products.Where(where);
            }
            return Products;
        }



        public async Task<Products> GetById(int Id)
        {
            return await _context.Products.Include(a=> a.Brand).Include(p => p.Category).FirstOrDefaultAsync(a => a.ProductId == Id);
        }

        public async Task Update(Products Products)
        {
            _context.Products.Update(Products);
            await _context.SaveChangesAsync();
        }


    }
}

