using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using SPR421_Shop.Models;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var viewModel = new CreateProductVM
            {
                Categories = _context.Categories
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductVM viewModel)
        {
            var model = new Product
            {
                Name = viewModel.Name!,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                Amount = viewModel.Amount,
                Price = viewModel.Price
            };

            _context.Products.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}