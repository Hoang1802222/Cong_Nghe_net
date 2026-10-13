using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;
using HTTGundamShop.ViewModels;

namespace HTTGundamShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly HTTShopContext _context;

        public OrderController(HTTShopContext context)
        {
            _context = context;
        }

        // Tạm thời cố định UserId = 1 để test đặt hàng
        private int GetCurrentUserId()
        {
            return 1;
        }

        // 1. Trang hiển thị form nhập thông tin thanh toán (GET: /Order/Checkout)
        public async Task<IActionResult> Checkout()
        {
            int userId = GetCurrentUserId();

            // Lấy giỏ hàng kèm theo sản phẩm của người dùng
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                // Giỏ hàng trống thì quay về trang Giỏ hàng
                return RedirectToAction("Index", "Cart");
            }

            var model = new CheckoutViewModel
            {
                CartItems = cart.CartItems.Select(item => new CartItemViewModel
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? "Sản phẩm",
                    ImageUrl = item.Product?.ImageUrl,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            return View(model);
        }

        // 2. Xử lý đặt hàng và lưu vào CSDL (POST: /Order/Checkout)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            int userId = GetCurrentUserId();

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                // Nếu dữ liệu nhập chưa hợp lệ, load lại danh sách sản phẩm để hiển thị lại view
                model.CartItems = cart.CartItems.Select(item => new CartItemViewModel
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? "Sản phẩm",
                    ImageUrl = item.Product?.ImageUrl,
                    Price = item.Product?.Price ?? 0,
                    Quantity = item.Quantity
                }).ToList();

                return View(model);
            }

            // Ghép thông tin Họ tên, SĐT, Địa chỉ, Ghi chú thành 1 chuỗi hoàn chỉnh cho ShippingAddress
            string fullAddress = $"{model.FullName} | SĐT: {model.Phone} | ĐC: {model.Address}";
            if (!string.IsNullOrWhiteSpace(model.Note))
            {
                fullAddress += $" | Ghi chú: {model.Note}";
            }

            // Tạo đối tượng Đơn hàng (Order)
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending", // Trạng thái chờ xử lý
                TotalAmount = cart.CartItems.Sum(i => (i.Product != null ? i.Product.Price : 0m) * i.Quantity),
                ShippingAddress = fullAddress
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Lưu Order trước để SQL Server tự cấp OrderId

            // Thêm từng chi tiết sản phẩm vào bảng OrderDetails
            foreach (var item in cart.CartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    UnitPrice = item.Product != null ? item.Product.Price : 0m,
                    Quantity = item.Quantity
                };
                _context.OrderDetails.Add(orderDetail);
            }

            // Dọn sạch các sản phẩm trong giỏ hàng sau khi đã đặt hàng thành công
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            // Chuyển hướng sang trang thông báo đặt hàng thành công
            return RedirectToAction("Success", new { id = order.OrderId });
        }

        // 3. Trang thông báo đặt hàng thành công (GET: /Order/Success/id)
        public IActionResult Success(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}