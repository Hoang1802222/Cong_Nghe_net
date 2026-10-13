using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;

namespace HTTGundamShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly HTTShopContext _context;

        public AdminHomeController(HTTShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Thống kê số liệu
            ViewBag.TotalOrders = await _context.Orders.CountAsync();
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();

            // Tính tổng doanh thu từ các đơn đã hoàn thành
            ViewBag.TotalRevenue = await _context.Orders
                .Where(o => o.OrderStatus == "Đã hoàn thành")
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            // 2. Lấy 5 đơn hàng mới nhất
            var recentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            return View(recentOrders);
        }
    }
}