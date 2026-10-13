using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductController : Controller
    {
        // 🛑 Đã sửa: Thay HTTGundamShopContext -> HTTShopContext
        private readonly HTTShopContext _context;

        public AdminProductController(HTTShopContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProduct
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Admin/AdminProduct/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Admin/AdminProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Admin/AdminProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Admin/AdminProduct/Delete/5 (Thực hiện xóa dữ liệu trong DB)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa sản phẩm thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}