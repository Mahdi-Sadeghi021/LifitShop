using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; } // Nullable چون ممکنه دسته اصلی باشه
        public Categories? ParentCategory { get; set; }
        public ICollection<Categories>? SubCategories { get; set; }
        public ICollection<Products>? Products { get; set; }
    }
}
