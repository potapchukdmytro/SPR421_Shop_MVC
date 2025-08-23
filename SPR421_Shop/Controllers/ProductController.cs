using Microsoft.AspNetCore.Mvc;

namespace SPR421_Shop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}