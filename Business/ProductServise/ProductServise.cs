using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.FileUploudServise;
using DataAccess.Models;
using DataAccess.Repositories.ProductRepsitory;
using Microsoft.EntityFrameworkCore;

namespace Business.ProductServise
{
    
    public class ProductServise
    {
        private readonly  IProductRepository _productRepository;
        private readonly IFileUploudservise _fileUploudservise;
        public ProductServise(IProductRepository productRepository, IFileUploudservise fileUploudservise)
        {
            _productRepository = productRepository;
            _fileUploudservise = fileUploudservise;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _productRepository.GetAll().ToListAsync();
        }
        public async Task<IEnumerable<Products>> GetProductwithcategorybrand(Expression<Func<Products, bool>> where = null)
        {
            return await _productRepository.GetAll(where).Include(a => a.Brand).Include(b=> b.Category).ToListAsync();
        }
        public async Task<Products> GetProductsById(int Id)
        {
            return await _productRepository.GetById(Id);
        }

        public async Task CrateProduct(ProductsDto productsDto)
        {
            var products = new Products
            {
                Description = productsDto.Description,
               
                BrandId = productsDto.BrandId,
                
                CategoryId = productsDto.CategoryId,
                IsAvailable = productsDto.IsAvailable,
                ProductId = productsDto.ProductId,
                ProductPrice = productsDto.ProductPrice,
                ProductTitle = productsDto.ProductTitle,
                Productcreated=DateTime.Now,

            };
            products.ImageName= await _fileUploudservise.UploudFileAsync(productsDto.ImageName);
            await _productRepository.Add(products);
        }
        public async Task UpdateProduct(ProductsDto productsDto)
        {
            var product = await GetProductsById(productsDto.ProductId);
            product.Description = productsDto.Description;

            product.BrandId = productsDto.BrandId;

            product.CategoryId = productsDto.CategoryId;
            product.IsAvailable = productsDto.IsAvailable;
            product.ProductId = productsDto.ProductId;
            product.ProductPrice = productsDto.ProductPrice;
            product.ProductTitle = productsDto.ProductTitle;
            product.ShowHomePage = productsDto.ShowHomePage;
            product.Productcreated = DateTime.Now;

            if(productsDto.ImageName != null)
            {
                product.ImageName = await _fileUploudservise.UploudFileAsync(productsDto.ImageName);
            }
     

            await _productRepository.Update(product);
        }
        public async Task DeleteProduct(Products products)
        {
            await _productRepository.Delete(products);
        }

        public async Task<ProductsDto> GetProductsDtoById(int Id)
        {
            var products= await _productRepository.GetById(Id);
            var productsDto = new ProductsDto()
            {
                Description = products.Description,

                BrandId = products.BrandId,

                CategoryId = products.CategoryId,
                IsAvailable = products.IsAvailable,
                ProductId = products.ProductId,
                ProductPrice = products.ProductPrice,
                ShowHomePage= products.ShowHomePage,
                ProductTitle = products.ProductTitle,
                ImagggeName=products.ImageName,
               
            };
            return productsDto;
        }
    }
}
