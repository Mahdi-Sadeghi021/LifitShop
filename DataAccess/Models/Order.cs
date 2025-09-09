using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Payed { get; set; }

        public int UserId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }

        public Status Status { get; set; } 

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
