using DataAccess.Models;

namespace LifitShop.Models
{
    public class HomeIndexViewModel
    {
        public List<Products> AllProducts { get; set; }
        public List<Products> LastProducts { get; set; }
    }
}
