using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom18.Models;

namespace nhom18.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly QuanLyQuanCafe1Context _context;

        public ThongKeController(QuanLyQuanCafe1Context context)
        {
            _context = context;
        }

        // GET: ThongKe/Index (Hiển thị báo cáo doanh thu và món bán chạy)
        public async Task<IActionResult> Index(DateTime? tuNgay, DateTime? denNgay)
        {
            // Mặc định lấy từ đầu tháng đến hiện tại nếu không chọn ngày
            var fromDate = tuNgay ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var toDate = denNgay ?? DateTime.Now.Date.AddDays(1).AddSeconds(-1); // Hết ngày hiện tại

            // Lấy danh sách các hóa đơn đã thanh toán trong khoảng thời gian
            var ordersQuery = _context.Orders
                .Include(o => o.ChiTietOrders)
                .ThenInclude(ct => ct.MaMonNavigation)
                .Where(o => o.TrangThai == "Đã thanh toán" && o.GioThanhToan >= fromDate && o.GioThanhToan <= toDate);

            var listOrders = await ordersQuery.ToListAsync();

            // Tính các chỉ số tổng quan
            decimal tongDoanhThu = listOrders.Sum(o => o.TongTien);
            int tongSoHoaDon = listOrders.Count();

            // Thống kê món bán chạy (Gom nhóm theo MaMon và tính tổng số lượng bán)
            var monBanChay = listOrders
                .SelectMany(o => o.ChiTietOrders)
                .GroupBy(ct => ct.MaMonNavigation.TenMon)
                .Select(g => new MonBanChayViewModel
                {
                    TenMon = g.Key,
                    SoLuongBan = g.Sum(x => x.SoLuong),
                    DoanhThuMon = g.Sum(x => x.SoLuong * x.DonGia)
                })
                .OrderByDescending(x => x.SoLuongBan)
                .Take(5) // Lấy top 5 món bán chạy nhất
                .ToList();

            // Truyền dữ liệu sang View
            ViewBag.TuNgay = fromDate.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = toDate.ToString("yyyy-MM-dd");
            ViewBag.TongDoanhThu = tongDoanhThu;
            ViewBag.TongSoHoaDon = tongSoHoaDon;
            ViewBag.MonBanChay = monBanChay;

            return View(listOrders);
        }
    }

    // ViewModel phụ trợ để hiển thị thống kê món bán chạy
    public class MonBanChayViewModel
    {
        public string TenMon { get; set; } = null!;
        public int SoLuongBan { get; set; }
        public decimal DoanhThuMon { get; set; }
    }
}