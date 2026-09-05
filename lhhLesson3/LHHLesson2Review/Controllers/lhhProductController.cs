using Microsoft.AspNetCore.Mvc;
using LHHLesson2Review.Models;
namespace LHHLesson2Review.Controllers
{
    public class lhhProductController : Controller
    {
        public IActionResult lhhProduct()
        {
            ViewBag.name = "Le Huy hoang";
            ViewData["age"] = 20;
            TempData["address"] = "Ha Noi";
            return View();
        }
        public IActionResult getProduct()
        {
            lhhProduct product = new lhhProduct()
            {
                productID = "2410900036",
                productName = "Le Huy Hoang",
                YearRelease = 2006,
                price = 1000
            };
            ViewBag.product = product;
            ViewData["product"] = product;
            return View(product);
        }
    }
}
