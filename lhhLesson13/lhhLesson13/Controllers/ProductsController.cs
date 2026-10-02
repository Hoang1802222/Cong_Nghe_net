using Microsoft.AspNetCore.Mvc;

namespace lhhLesson13.Controllers
{
    public class ProductsController : Controller
    {
        static List<Models.Product> products = new List<Models.Product>
        {
            new Models.Product { Id = 1, Name = "Product 1", Price = 10.0m },
            new Models.Product { Id = 2, Name = "Product 2", Price = 20.0m },
            new Models.Product { Id = 3, Name = "Product 3", Price = 30.0m }
        };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Hots()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }
    }
}
