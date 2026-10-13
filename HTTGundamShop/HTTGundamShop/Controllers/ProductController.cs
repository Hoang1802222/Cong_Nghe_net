using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly HTTShopContext _context;

        public ProductController(HTTShopContext context)
        {
            _context = context;
        }

        // 1. Trang danh sách sản phẩm, lọc & tìm kiếm
        public async Task<IActionResult> Index(int? categoryId, string search, string sortBy)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Lọc theo danh mục nếu có chọn
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
                ViewBag.CurrentCategoryId = categoryId.Value;
            }

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search));
                ViewBag.SearchKeyword = search;
            }

            // Sắp xếp sản phẩm
            ViewBag.CurrentSort = sortBy;
            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_asc" => query.OrderBy(p => p.ProductName),
                "name_desc" => query.OrderByDescending(p => p.ProductName),
                _ => query.OrderByDescending(p => p.ProductId) // Mới nhất lên đầu
            };

            // Lấy tất cả danh mục để hiển thị thanh lọc bên trái
            ViewBag.Categories = await _context.Categories.ToListAsync();

            var products = await query.ToListAsync();
            return View(products);
        }

        // 2. Trang chi tiết sản phẩm
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) return NotFound();

            // Lấy 4 sản phẩm liên quan cùng danh mục (loại trừ sản phẩm hiện tại)
            ViewBag.RelatedProducts = await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != product.ProductId)
                .Take(4)
                .ToListAsync();

            return View(product);
        }
    }
}