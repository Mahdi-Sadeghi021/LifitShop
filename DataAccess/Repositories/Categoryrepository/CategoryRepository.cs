using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Categoryrepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Categoriesrepository
{
    public class CategoriesRepository : ICategoryRepository
    {
        private readonly LifitDbContext _context;

        public CategoriesRepository(LifitDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Categories category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Categories = await GetByIdAsync(id);
            _context.Categories.Remove(Categories);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await _context.Categories
           .Include(c => c.ParentCategory)
           .Include(c => c.SubCategories)
           .ToListAsync();
        }

        public async Task<Categories?> GetByIdAsync(int id)
        {
            return await _context.Categories
             .Include(c => c.SubCategories).Include(c => c.ParentCategory) // اگر ساختار درختی داری
             .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task UpdateAsync(Categories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Categories>> GetAllAsync()
        //{

        //}

        //public async Task<Categories> GetByIdAsync(int id)
        //{

        //}

        //public async Task AddAsync(Categories Categories)
        //{

        //}

        //public async Task UpdateAsync(Categories Categories)
        //{

        //}

        //public async Task DeleteAsync(Categories Categories)
        //{
     
        //}

        //public async Task DeleteAsync(int id)
        //{

        //}
    }
}

