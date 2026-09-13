using lhhLesson04.Models;
using Microsoft.AspNetCore.Mvc;

namespace lhhLesson04.Controllers
{
    public class lhhProductController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = new List<Product>
            {
                new Product()
                {
                    ProductID = "1",
                    ProductName = "Product RX-78-2",
                    ProductImages = Url.Content("~/images/Product1.jpg"),
                    ProductPrice = 1000,
                    ProductSalePrice = 800,
                    CategoryId = "1",
                    ProductDescription = "The original Product model.",
                    ProductStatus = "Available",
                    ProductCreatedDate = DateTime.Now
                },

                new Product()
                {
                    ProductID = "2",
                    ProductName = "Product Wing Zero",
                    ProductImages = Url.Content("~/images/Product2.jpg"),
                    ProductPrice = 1200,
                    ProductSalePrice = 1000,
                    CategoryId = "2",
                    ProductDescription = "A powerful Product from the Wing series.",
                    ProductStatus = "Available",
                    ProductCreatedDate = DateTime.Now
                },

                new Product()
                {
                    ProductID = "3",
                    ProductName = "Product Unicorn",
                    ProductImages = Url.Content("~/images/Product3.jpg"),
                    ProductPrice = 1500,
                    ProductSalePrice = 1300,
                    CategoryId = "3",
                    ProductDescription = "A unique Product with a transforming feature.",
                    ProductStatus = "Available",
                    ProductCreatedDate = DateTime.Now
                }
            };

            List<Category> categories = new List<Category>
            {
                new Category()
                {
                    CategoryID = "1",
                    CategoryName = "Mobile Suit Gundam"
                },

                new Category()
                {
                    CategoryID = "2",
                    CategoryName = "Gundam Wing"
                },

                new Category()
                {
                    CategoryID = "3",
                    CategoryName = "Gundam Unicorn"
                }
            };

            ViewBag.Products = products;
            ViewBag.Categories = categories;

            return View();
        }
    }
}
