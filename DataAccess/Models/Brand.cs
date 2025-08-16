using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Brand
    {

        [Key]
        public int BrandId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }

        public ICollection<Products>? Products { get; set; }
    }
}
