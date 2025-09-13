using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SPR421_Shop.Models;
using SPR421_Shop.Repositories.Product;
using SPR421_Shop.Services;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly PaginationVM _pagination;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IProductRepository productRepository, IOptions<PaginationVM> pagination)
        {
            _logger = logger;
            _context = context;
            _productRepository = productRepository;
            _pagination = pagination.Value;
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

        public IActionResult AddToCart(int productId)
        {
            if(productId <= 0)
            {
                return NotFound();
            }

            HttpContext.Session.AddToCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Index(string? category, int page = 1)
        {
            _pagination.Page = page;

            var products = !string.IsNullOrEmpty(category)
                ? _productRepository.GetByCategory(category, _pagination)
                : _productRepository.GetByPagination(_pagination);

            var viewModel = new HomeVM
            {
                Products = products,
                Categories = _context.Categories,
                Pagination = _pagination,
                Category = category
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
