using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class ClientController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;

        public ClientController(QuanLyQuanCafe1Context context)
        {
            _context = context;
        }

        // GET: Client/Menu
        public async Task<IActionResult> Menu(int? maDm)
        {
            // Lấy danh sách danh mục sắp xếp theo thứ tự
            var danhMucs = await _context.DanhMucMons.OrderBy(d => d.ThuTu).ToListAsync();

            // Lấy toàn bộ danh sách món ăn từ CSDL
            var mons = _context.Mons.AsQueryable();

            // Nếu người dùng chọn một danh mục cụ thể thì lọc theo MaDm
            if (maDm.HasValue && maDm.Value > 0)
            {
                mons = mons.Where(m => m.MaDm == maDm.Value);
            }

            ViewBag.DanhMucs = danhMucs;
            ViewBag.SelectedDanhMuc = maDm;

            return View(await mons.ToListAsync());
        }

        // GET: Client/DatBan
        public async Task<IActionResult> DatBan()
        {
            ViewBag.DanhSachBan = await _context.Bans.Where(b => b.TrangThai == "Trống").ToListAsync();
            return View();
        }

        // POST: Client/DatBan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatBan(string hoTen, string soDienThoai, int soNguoi, DateTime thoiGianDat, int? maBan, string? ghiChu)
        {
            if (thoiGianDat < DateTime.Now)
            {
                ModelState.AddModelError("", "Thời gian đặt bàn phải từ thời điểm hiện tại trở đi!");
                ViewBag.DanhSachBan = await _context.Bans.Where(b => b.TrangThai == "Trống").ToListAsync();
                return View();
            }

            if (maBan.HasValue)
            {
                var ban = await _context.Bans.FindAsync(maBan.Value);
                if (ban != null)
                {
                    ban.TrangThai = "Đã đặt";
                    _context.Bans.Update(ban);

                    var order = new Order
                    {
                        MaBan = maBan.Value,
                        LoaiOrder = "Đặt trước",
                        GioVao = thoiGianDat,
                        TrangThai = "Đã đặt",
                        TongTien = 0
                    };
                    _context.Orders.Add(order);
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã đặt bàn thành công cho {hoTen}!";
            return RedirectToAction(nameof(Menu));
        }
    }
}