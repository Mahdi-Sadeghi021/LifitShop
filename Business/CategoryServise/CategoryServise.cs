using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repositories.Categoryrepository;

namespace Business.CategoryServise
{
    public class CategoryServise
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServise( ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Categories>> GetCategory()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Categories> GetCategoryById(int Id)
        {
            return await _categoryRepository.GetByIdAsync(Id);
        }

        public async Task CrateCategory(Categories categories)
        {
            await _categoryRepository.AddAsync(categories);
        }
        public async Task UpdateCategory(Categories categories)
        {
            await _categoryRepository.UpdateAsync(categories);
        }
        public async Task DeleteCategory(Categories categories)
        {
            await _categoryRepository.DeleteAsync(categories);
        }

    }
}
