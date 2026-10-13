using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Areas.Admin.Controllers
{
    [Area("Admin")] // 👈 RẤT QUAN TRỌNG
    public class AdminOrderController : Controller
    {
        private readonly HTTShopContext _context;

        public AdminOrderController(HTTShopContext context)
        {
            _context = context;
        }

        // 1. Danh sách đơn hàng
        public async Task<IActionResult> Index(string status)
        {
            var query = _context.Orders.Include(o => o.User).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.OrderStatus == status);
            }

            var orders = await query.OrderByDescending(o => o.OrderDate).ToListAsync();
            ViewBag.CurrentStatus = status;

            return View(orders);
        }

        // 2. Xem chi tiết đơn hàng
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // 3. Cập nhật trạng thái
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.OrderStatus = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái đơn hàng #{id} thành '{status}'!";
            }
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}