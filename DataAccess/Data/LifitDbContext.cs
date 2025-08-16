using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class LifitDbContext : DbContext
    {
        public LifitDbContext(DbContextOptions<LifitDbContext> options) : base(options)
        {
   
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // همیشه اینو صدا بزن

            // تعریف رابطه دسته‌-زیر‌دسته
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.SubCategories)         // هر دسته می‌تونه چند زیر‌دسته داشته باشه
                .WithOne(c => c.ParentCategory)        // هر زیر‌دسته فقط یک دسته پدر داره
                .HasForeignKey(c => c.ParentCategoryId) // کلید خارجی برای رابطه
                .OnDelete(DeleteBehavior.Restrict);    // جلوگیری از حذف cascade
        }

    }
}
