using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repositories.Categoryrepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories?> GetByIdAsync(int id);
        Task AddAsync(Categories category);
        Task UpdateAsync(Categories category);
        Task DeleteAsync(Categories category);
        Task DeleteAsync(int id);
    }
}
