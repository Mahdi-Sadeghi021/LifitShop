using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repositories.BrandRepository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAll();

        Task<Brand> GetById(int Id);

        Task Add(Brand brand);
        Task Update(Brand brand);
        Task Delete(Brand brand);
        Task Delete(int Id);
    }
}
