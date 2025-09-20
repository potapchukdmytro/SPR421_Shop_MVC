using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR421_Shop.Models;
using SPR421_Shop.Repositories.Product;
using SPR421_Shop.Services;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult IncreaseCount(int productId)
        {
            var product = _productRepository.Products
                .FirstOrDefault(p => p.Id == productId);

            if(product != null)
            {
                HttpContext.Session.IncreaseCount(productId, product.Amount);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DecreaseCount(int productId)
        {
            HttpContext.Session.DecreaseCount(productId, 1);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            if (productId <= 0)
            {
                return NotFound();
            }

            HttpContext.Session.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var sessionItems = HttpContext.Session.CartItems();

            IEnumerable<Product> query = _productRepository.Products
                .Include(p => p.Category)
                .ToList()
                .Where(p => sessionItems.Any(i => i.ProductId == p.Id));

            var cartVm = query
                .Select(p => new CartVM
                {
                    Product = p,
                    Count = sessionItems
                    .FirstOrDefault(i => i.ProductId == p.Id)!.Count
                }).AsEnumerable();

            return View(cartVm);
        }
    }
}
