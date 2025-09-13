using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPR421_Shop.Models;
using SPR421_Shop.Services;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AddToCart(int productId)
        {
            if(productId == 0)
            {
                return NotFound();
            }

            HttpContext.Session.AddToCart(productId);

            return RedirectToAction("Index");
        }

        public IActionResult Index(string? category)
        {
            IQueryable<Product> products = _context.Products;

            if(!string.IsNullOrEmpty(category))
            {
                products = products
                    .Where(p => p.Category != null && p.Category.Name.ToLower() == category.ToLower());
            }

            var viewModel = new HomeVM
            {
                Products = products,
                Categories = _context.Categories
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
