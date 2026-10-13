using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly HTTShopContext _context;

        public AdminCategoryController(HTTShopContext context)
        {
            _context = context;
        }

        // 1. Danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products) // Nạp sản phẩm để đếm số lượng
                .ToListAsync();

            return View(categories);
        }

        // 2. Thêm danh mục (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Thêm danh mục (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm danh mục mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // 3. Sửa danh mục (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // Sửa danh mục (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(e => e.CategoryId == category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // 4. Xóa danh mục (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // Xóa danh mục (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CategoryId)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == CategoryId);

            if (category != null)
            {
                // Rào chắn an toàn: Không cho xóa nếu danh mục đang có sản phẩm
                if (category.Products != null && category.Products.Any())
                {
                    TempData["ErrorMessage"] = $"Không thể xóa danh mục '{category.CategoryName}' vì đang chứa {category.Products.Count} sản phẩm!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa danh mục thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
