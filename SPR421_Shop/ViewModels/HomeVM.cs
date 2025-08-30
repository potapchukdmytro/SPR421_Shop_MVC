using SPR421_Shop.Models;

namespace SPR421_Shop.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
