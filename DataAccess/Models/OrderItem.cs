using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int BasketId { get; set; }

        // محصولی که کاربر انتخاب کرده
        public int ProductId { get; set; }

        // قیمت لحظه‌ای محصول (که موقع خرید ذخیره میشه)
        public decimal UnitPrice { get; set; }

        // تعداد محصول
        public int Quantity { get; set; }

        // مبلغ کل (قابل محاسبه هم هست)
        public decimal TotalPrice => UnitPrice * Quantity;

        public DateTime Created { get; set; } = DateTime.Now;

        // رابطه‌ها
        [ForeignKey("OrderId")]
        public Order order { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}

