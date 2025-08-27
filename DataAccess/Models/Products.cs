using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductTitle { get; set; }

        public DateTime Productcreated { get; set; }
        public bool ShowHomePage { get; set; }

        public string? Description { get; set; }


        [Required]
        public decimal ProductPrice { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories? Category { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        // تصویر یا ویژگی‌های دیگر
        public string? ImageName { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
