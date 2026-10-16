using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class MonController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MonController(QuanLyQuanCafe1Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Mon (Có tìm kiếm, lọc theo danh mục, giá và phân trang)
        public async Task<IActionResult> Index(string searchString, int? maDm, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            int pageSize = 8; // Số món hiển thị trên 1 trang
            var mons = _context.Mons.Include(m => m.MaDmNavigation).AsQueryable();

            // Tìm kiếm theo tên món
            if (!string.IsNullOrEmpty(searchString))
            {
                mons = mons.Where(m => m.TenMon.Contains(searchString));
            }

            // Lọc theo danh mục
            if (maDm.HasValue && maDm > 0)
            {
                mons = mons.Where(m => m.MaDm == maDm.Value);
            }

            // Lọc theo khoảng giá
            if (minPrice.HasValue) mons = mons.Where(m => m.GiaBan >= minPrice.Value);
            if (maxPrice.HasValue) mons = mons.Where(m => m.GiaBan <= maxPrice.Value);

            ViewBag.DanhSachDm = new SelectList(_context.DanhMucMons, "MaDm", "TenDm", maDm);
            ViewBag.CurrentSearch = searchString;

            // Phân trang
            int totalItems = await mons.CountAsync();
            var pagedData = await mons.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(pagedData);
        }

        // GET: Mon/Create
        public IActionResult Create()
        {
            ViewBag.MaDm = new SelectList(_context.DanhMucMons, "MaDm", "TenDm");
            return View();
        }

        // POST: Mon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mon mon, IFormFile? anhMonFile)
        {
            // Bỏ qua validate các thuộc tính điều hướng của EF Core
            ModelState.Remove("MaDmNavigation");
            ModelState.Remove("ChiTietOrders");

            if (mon.MaDm <= 0)
            {
                ModelState.AddModelError("MaDm", "Vui lòng chọn danh mục món ăn!");
            }

            if (mon.GiaBan <= 0)
            {
                ModelState.AddModelError("GiaBan", "Giá món phải lớn hơn 0!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload ảnh nếu có chọn file
                    if (anhMonFile != null && anhMonFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + anhMonFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await anhMonFile.CopyToAsync(fileStream);
                        }
                        mon.AnhMon = "/images/" + uniqueFileName;
                    }
                    else
                    {
                        mon.AnhMon = "";
                    }

                    if (string.IsNullOrEmpty(mon.TrangThai))
                    {
                        mon.TrangThai = "Đang bán";
                    }

                    _context.Add(mon);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm món mới thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    ModelState.AddModelError(string.Empty, "Lỗi cơ sở dữ liệu: " + errorMsg);
                }
            }

            ViewBag.MaDm = new SelectList(_context.DanhMucMons, "MaDm", "TenDm", mon.MaDm);
            return View(mon);
        }

        // GET: Mon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mon = await _context.Mons.FindAsync(id);
            if (mon == null) return NotFound();

            ViewBag.MaDm = new SelectList(_context.DanhMucMons, "MaDm", "TenDm", mon.MaDm);
            return View(mon);
        }

        // POST: Mon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mon mon, IFormFile? anhMonFile)
        {
            if (id != mon.MaMon) return NotFound();

            // Bỏ qua validate các thuộc tính điều hướng của EF Core
            ModelState.Remove("MaDmNavigation");
            ModelState.Remove("ChiTietOrders");

            if (mon.GiaBan <= 0)
            {
                ModelState.AddModelError("GiaBan", "Giá món phải lớn hơn 0!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMon = await _context.Mons.AsNoTracking().FirstOrDefaultAsync(m => m.MaMon == id);
                    if (existingMon == null) return NotFound();

                    if (anhMonFile != null && anhMonFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + anhMonFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await anhMonFile.CopyToAsync(fileStream);
                        }
                        mon.AnhMon = "/images/" + uniqueFileName;
                    }
                    else
                    {
                        mon.AnhMon = existingMon.AnhMon;
                    }

                    if (string.IsNullOrEmpty(mon.TrangThai))
                    {
                        mon.TrangThai = "Đang bán";
                    }

                    _context.Update(mon);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật món thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Mons.Any(e => e.MaMon == id)) return NotFound();
                    else throw;
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    ModelState.AddModelError(string.Empty, "Lỗi cơ sở dữ liệu: " + errorMsg);
                }
            }

            ViewBag.MaDm = new SelectList(_context.DanhMucMons, "MaDm", "TenDm", mon.MaDm);
            return View(mon);
        }

        // POST: Xóa hoặc chuyển trạng thái "Ngừng bán" (Đúng yêu cầu đề bài)
        [HttpPost]
        public async Task<IActionResult> XoaHoacNgungBan(int id)
        {
            var mon = await _context.Mons.FindAsync(id);
            if (mon == null) return NotFound();

            // Kiểm tra xem món này đã có trong bảng ChiTietOrder nào chưa
            bool daCoTrongOrder = await _context.ChiTietOrders.AnyAsync(ct => ct.MaMon == id);

            if (daCoTrongOrder)
            {
                // Món đã có trong order chỉ được chuyển trạng thái "Ngừng bán"
                mon.TrangThai = "Ngừng bán";
                TempData["Warning"] = "Món đã phát sinh giao dịch trong hóa đơn, hệ thống đã tự chuyển sang trạng thái 'Ngừng bán'!";
            }
            else
            {
                // Chưa có trong order thì được phép xóa hẳn
                _context.Mons.Remove(mon);
                TempData["Success"] = "Đã xóa món thành công!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}