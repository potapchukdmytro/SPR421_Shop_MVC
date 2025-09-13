using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPR421_Shop.Models;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private string? SaveImage(IFormFile image)
        {
            try
            {
                var types = image.ContentType.Split('/');

                if (types.Length != 2 || types[0] != "image")
                {
                    return null;
                }

                string imageName = $"{Guid.NewGuid().ToString()}.{types[1]}";
                string rootPath = _environment.WebRootPath;
                string imagePath = Path.Combine(rootPath, "images", imageName);

                using (var fileStream = System.IO.File.Create(imagePath))
                {
                    using (var imageStream = image.OpenReadStream())
                    {
                        imageStream.CopyTo(fileStream);
                    }
                }

                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
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
        public IActionResult Create([FromForm] CreateProductVM viewModel)
        {
            var model = new Product
            {
                Name = viewModel.Name!,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                Amount = viewModel.Amount,
                Price = viewModel.Price
            };

            if (viewModel.Image != null)
            {
                model.Image = SaveImage(viewModel.Image);
            }

            _context.Products.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}