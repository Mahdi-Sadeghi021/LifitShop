using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {

        private readonly LifitDbContext _context;
        public BrandRepository(LifitDbContext context)
        {
            _context=context;
        }
        public async Task Add(Brand brand)
        {
          _context.Brands.Add(brand);
          await _context.SaveChangesAsync();
        }

        public async Task Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var item = await GetById(Id);
            _context.Brands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            var item = await _context.Brands.ToListAsync();
            return item;
        }

        public async Task<Brand> GetById(int Id)
        {
            return await _context.Brands.FindAsync(Id); ;
        }

        public async Task Update(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }
    }
}
