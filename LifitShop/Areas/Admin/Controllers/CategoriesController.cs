using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Models;
using Business.CategoryServise;

namespace LifitShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryServise _categoryServise;

        public CategoriesController(CategoryServise categoryServise)
        {
            _categoryServise = categoryServise;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
           
            return View(await _categoryServise.GetCategory());
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryServise.GetCategoryById(id.Value);
                
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/Categories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(
                await _categoryServise.GetCategory(),
                "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,ParentCategoryId")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                await _categoryServise.CrateCategory(categories);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", categories?.ParentCategoryId);
            return View(categories);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryServise.GetCategoryById(id.Value);
            if (categories == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", categories?.ParentCategoryId);
            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,ParentCategoryId")] Categories categories)
        {
            if (id != categories.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryServise.UpdateCategory(categories);
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryServise.GetCategory(), "CategoryId", "CategoryName", categories.ParentCategoryId);
            return View(categories);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryServise.GetCategoryById(id.Value);
              
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _categoryServise.GetCategoryById(id);
            await _categoryServise.DeleteCategory(categories);
       
            return RedirectToAction(nameof(Index));
        }

   
    }
}
