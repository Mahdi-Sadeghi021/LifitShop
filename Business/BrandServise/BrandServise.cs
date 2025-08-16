using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repositories.BrandRepository;

namespace Business.BrandServise
{
    public class BrandServise
    {
        private readonly IBrandRepository _brandRepository;

        public BrandServise(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _brandRepository.GetAll();
        }
        public async Task<Brand> GetBrandsById(int Id)
        {
            return await _brandRepository.GetById(Id);
        }

        public async Task CrateBrand(Brand brand)
        {
            await _brandRepository.Add(brand);
        }
        public async Task UpdateBrand(Brand brand)
        {
            await _brandRepository.Update(brand);
        }
        public async Task DeleteBrand(Brand brand)
        {
            await _brandRepository.Delete(brand);
        }
    }
}
