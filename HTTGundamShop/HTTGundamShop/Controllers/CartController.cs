using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTTGundamShop.Models;
using HTTGundamShop.ViewModels;

namespace HTTGundamShop.Controllers
{
    public class CartController : Controller
    {
        private readonly HTTShopContext _context;

        public CartController(HTTShopContext context)
        {
            _context = context;
        }

        // Tạm thời cố định UserId = 1 để test giỏ hàng
        private int GetCurrentUserId()
        {
            return 1;
        }

        // 1. Xem giỏ hàng
        public async Task<IActionResult> Index()
        {
            int userId = GetCurrentUserId();

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return View(new List<CartItemViewModel>());
            }

            var cartViewModels = cart.CartItems.Select(item => new CartItemViewModel
            {
                CartItemId = item.CartItemId,
                ProductId = item.ProductId,
                ProductName = item.Product?.ProductName ?? "Sản phẩm",
                ImageUrl = item.Product?.ImageUrl,
                Price = item.Product?.Price ?? 0,
                Quantity = item.Quantity
            }).ToList();

            return View(cartViewModels);
        }

        // 2. Thêm vào giỏ
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            int userId = GetCurrentUserId();

            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 3. Cập nhật số lượng
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // 4. Xóa khỏi giỏ
        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}