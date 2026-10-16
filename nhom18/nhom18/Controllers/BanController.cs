using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class BanController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;

        public BanController(QuanLyQuanCafe1Context context)
        {
            _context = context;
        }

        // GET: Ban (Hiển thị danh sách bàn, có thể lọc theo khu vực)
        public async Task<IActionResult> Index(string searchString, string khuVuc)
        {
            var bans = _context.Bans.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bans = bans.Where(b => b.TenBan.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(khuVuc))
            {
                bans = bans.Where(b => b.KhuVuc == khuVuc);
            }

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentKhuVuc = khuVuc;
            ViewBag.DanhSachKhuVuc = await _context.Bans.Select(b => b.KhuVuc).Distinct().ToListAsync();

            return View(await bans.ToListAsync());
        }

        // GET: Ban/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ban/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ban ban)
        {
            ModelState.Remove("Orders");

            if (string.IsNullOrEmpty(ban.TrangThai))
            {
                ban.TrangThai = "Trống"; // Mặc định bàn mới tạo là Trống
            }

            if (ModelState.IsValid)
            {
                _context.Add(ban);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm bàn mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(ban);
        }

        // GET: Ban/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ban = await _context.Bans.FindAsync(id);
            if (ban == null) return NotFound();

            return View(ban);
        }

        // POST: Ban/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ban ban)
        {
            if (id != ban.MaBan) return NotFound();

            ModelState.Remove("Orders");

            if (string.IsNullOrEmpty(ban.TrangThai))
            {
                ban.TrangThai = "Trống";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ban);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thông tin bàn thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bans.Any(e => e.MaBan == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ban);
        }

        // POST: Ban/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ban = await _context.Bans.Include(b => b.Orders).FirstOrDefaultAsync(b => b.MaBan == id);
            if (ban == null) return NotFound();

            // Kiểm tra xem bàn đã có lịch sử order hóa đơn chưa
            if (ban.Orders.Any())
            {
                TempData["Error"] = "Không thể xóa bàn đã phát sinh hóa đơn giao dịch!";
                return RedirectToAction(nameof(Index));
            }

            _context.Bans.Remove(ban);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa bàn thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}