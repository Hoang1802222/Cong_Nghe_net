using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly HTTShopContext _context;

        public AdminUserController(HTTShopContext context)
        {
            _context = context;
        }

        // 1. Danh sách người dùng
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.Orders) // Lấy danh sách đơn hàng để đếm số đơn đã đặt
                .ToListAsync();

            return View(users);
        }

        // 2. Chi tiết người dùng & Lịch sử mua hàng
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null) return NotFound();

            return View(user);
        }

        // 3. Xóa người dùng (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null) return NotFound();

            return View(user);
        }

        // 4. Xóa người dùng (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa tài khoản thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}