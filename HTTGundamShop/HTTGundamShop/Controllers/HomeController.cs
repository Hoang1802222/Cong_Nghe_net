using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly HTTShopContext _context;

        public HomeController(HTTShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Danh sách Danh mục (Grades)
            ViewBag.Categories = await _context.Categories.ToListAsync();

            // 2. Sản phẩm Mới nhất (New Arrivals - 8 mẫu)
            ViewBag.NewArrivals = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Take(8)
                .ToListAsync();

            // 3. Sản phẩm HOT / Nổi bật (4 mẫu)
            ViewBag.FeaturedProducts = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Price) // Hoặc tùy biến tiêu chí
                .Take(4)
                .ToListAsync();

            return View();
        }
    }
}