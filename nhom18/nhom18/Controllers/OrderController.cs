using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class OrderController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;

        public OrderController(QuanLyQuanCafe1Context context)
        {
            _context = context;
        }

        // GET: Order (Danh sách các hóa đơn)
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.MaBanNavigation)
                .Include(o => o.ChiTietOrders)
                .ThenInclude(ct => ct.MaMonNavigation)
                .OrderByDescending(o => o.GioVao)
                .ToListAsync();

            return View(orders);
        }

        // GET: Order/Create?maBan=5
        public async Task<IActionResult> Create(int maBan)
        {
            var ban = await _context.Bans.FindAsync(maBan);
            if (ban == null) return NotFound();

            var existingOrder = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .ThenInclude(ct => ct.MaMonNavigation)
                .FirstOrDefaultAsync(o => o.MaBan == maBan && o.TrangThai == "Chưa thanh toán");

            ViewBag.Ban = ban;
            ViewBag.DanhSachMon = await _context.Mons.Where(m => m.TrangThai == "Đang bán").ToListAsync();
            ViewBag.DanhSachDanhMuc = await _context.DanhMucMons.OrderBy(d => d.ThuTu).ToListAsync();

            return View(existingOrder);
        }

        // POST: Order/ThemMon
        [HttpPost]
        public async Task<IActionResult> ThemMon(int maBan, int maMon, int soLuong, string? ghiChu)
        {
            if (soLuong <= 0) soLuong = 1;

            var order = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .FirstOrDefaultAsync(o => o.MaBan == maBan && o.TrangThai == "Chưa thanh toán");

            if (order == null)
            {
                order = new Order
                {
                    MaBan = maBan,
                    LoaiOrder = "Tại quán",
                    GioVao = DateTime.Now,
                    TrangThai = "Chưa thanh toán",
                    TongTien = 0
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var ban = await _context.Bans.FindAsync(maBan);
                if (ban != null)
                {
                    ban.TrangThai = "Đang phục vụ";
                    _context.Bans.Update(ban);
                }
            }

            var mon = await _context.Mons.FindAsync(maMon);
            if (mon == null) return NotFound();

            var chiTiet = new ChiTietOrder
            {
                MaOrder = order.MaOrder,
                MaMon = maMon,
                SoLuong = soLuong,
                DonGia = mon.GiaBan,
                GhiChu = ghiChu
            };

            _context.ChiTietOrders.Add(chiTiet);
            await _context.SaveChangesAsync();

            await TinhLaiTongTien(order.MaOrder);

            TempData["Success"] = $"Đã thêm {mon.TenMon} vào hóa đơn!";
            return RedirectToAction(nameof(Create), new { maBan = maBan });
        }

        // POST: Order/XoaMonChiTiet
        [HttpPost]
        public async Task<IActionResult> XoaMonChiTiet(int maOrder, int maMon, int maBan)
        {
            var chiTiet = await _context.ChiTietOrders
                .FirstOrDefaultAsync(ct => ct.MaOrder == maOrder && ct.MaMon == maMon);

            if (chiTiet != null)
            {
                _context.ChiTietOrders.Remove(chiTiet);
                await _context.SaveChangesAsync();

                await TinhLaiTongTien(maOrder);
                TempData["Success"] = "Đã xóa món khỏi hóa đơn!";
            }
            return RedirectToAction(nameof(Create), new { maBan = maBan });
        }

        // POST: Order/ThanhToan
        [HttpPost]
        public async Task<IActionResult> ThanhToan(int maOrder)
        {
            var order = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .FirstOrDefaultAsync(o => o.MaOrder == maOrder);

            if (order == null) return NotFound();

            order.TrangThai = "Đã thanh toán";
            order.GioThanhToan = DateTime.Now;
            _context.Orders.Update(order);

            if (order.MaBan.HasValue)
            {
                var ban = await _context.Bans.FindAsync(order.MaBan.Value);
                if (ban != null)
                {
                    ban.TrangThai = "Trống";
                    _context.Bans.Update(ban);
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Thanh toán hóa đơn thành công!";
            return RedirectToAction("Index", "Ban");
        }

        private async Task TinhLaiTongTien(int maOrder)
        {
            var order = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .FirstOrDefaultAsync(o => o.MaOrder == maOrder);

            if (order != null)
            {
                order.TongTien = order.ChiTietOrders.Sum(ct => ct.SoLuong * ct.DonGia);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}