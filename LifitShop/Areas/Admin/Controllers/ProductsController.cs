using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Models;
using Business.ProductServise;
using Business.BrandServise;
using Business.CategoryServise;

namespace LifitShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : AdminBaseController
    {
        private readonly ProductServise _productServise ;
        private readonly BrandServise _brandServise;
        private readonly CategoryServise _categoryServise;

        public ProductsController(ProductServise productServise, BrandServise brandServise, CategoryServise categoryServise)
        {
            _productServise = productServise;
            _brandServise = brandServise;
            _categoryServise = categoryServise;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var data = await _productServise.GetProductwithcategorybrand();
            return View(data);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productServise.GetProductwithcategorybrand(a => a.ProductId == id);
            var product= products.FirstOrDefault();
             
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BrandId"] = new SelectList(await _brandServise.GetBrands(), "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductsDto products)
        {
            if (ModelState.IsValid)
            {
               
                await _productServise.CrateProduct(products);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(await _brandServise.GetBrands(), "BrandId", "BrandName", products.BrandId);
            ViewData["CategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productServise.GetProductsDtoById(id.Value);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(await _brandServise.GetBrands(), "BrandId", "BrandName", products.BrandId);
            ViewData["CategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ProductsDto products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _productServise.UpdateProduct(products);
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(await _brandServise.GetBrands(), "BrandId", "BrandName", products.BrandId);
            ViewData["CategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productServise.GetProductwithcategorybrand(a => a.ProductId == id);
            var product = products.FirstOrDefault();
            if (products == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _productServise.GetProductwithcategorybrand(a => a.ProductId == id);
            var product = products.FirstOrDefault();
            await _productServise.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }




        //[Bind("ProductId,ProductTitle,Description,ProductPrice,CategoryId,BrandId,ImageName,IsAvailable")]
    }
}
