using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class DanhMucMonController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;

        public DanhMucMonController(QuanLyQuanCafe1Context context)
        {
            _context = context;
        }

        // GET: DanhMucMon
        public async Task<IActionResult> Index()
        {
            var danhMucs = await _context.DanhMucMons.ToListAsync();
            return View(danhMucs);
        }

        // GET: DanhMucMon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucMon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDm,TenDm,ThuTu")] DanhMucMon danhMucMon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucMon);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm danh mục thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucMon);
        }

        // GET: DanhMucMon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var danhMucMon = await _context.DanhMucMons.FindAsync(id);
            if (danhMucMon == null) return NotFound();

            return View(danhMucMon);
        }

        // POST: DanhMucMon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDm,TenDm,ThuTu")] DanhMucMon danhMucMon)
        {
            if (id != danhMucMon.MaDm) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucMon);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật danh mục thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DanhMucMons.Any(e => e.MaDm == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucMon);
        }

        // POST: DanhMucMon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucMon = await _context.DanhMucMons.Include(d => d.Mons).FirstOrDefaultAsync(d => d.MaDm == id);
            if (danhMucMon == null) return NotFound();

            // Kiểm tra xem danh mục có đang chứa món nào không
            if (danhMucMon.Mons.Any())
            {
                TempData["Error"] = "Không thể xóa danh mục đang chứa món ăn!";
                return RedirectToAction(nameof(Index));
            }

            _context.DanhMucMons.Remove(danhMucMon);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa danh mục thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}