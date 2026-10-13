using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HTTGundamShop.Models;
using HTTGundamShop.ViewModels;

namespace HTTGundamShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly HTTShopContext _context;

        public AccountController(HTTShopContext context)
        {
            _context = context;
        }

        // ================= TRANG ĐĂNG KÝ =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem UserName hoặc Email đã tồn tại chưa
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == model.Username || u.Email == model.Email);

                if (existingUser != null)
                {
                    if (existingUser.UserName == model.Username)
                    {
                        ModelState.AddModelError("Username", "Tên đăng nhập này đã được sử dụng!");
                    }
                    if (existingUser.Email == model.Email)
                    {
                        ModelState.AddModelError("Email", "Email này đã được sử dụng!");
                    }
                    return View(model);
                }

                // Tạo đối tượng User mới
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,        
                    Password = model.Password,
                    Address = model.Address,   
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        // ================= TRANG ĐĂNG NHẬP =================
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Tìm người dùng theo UserName và Password
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Tạo danh sách Claims thông tin người dùng
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email ?? ""),
                        new Claim(ClaimTypes.Role, user.Role ?? "User")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    // Đăng nhập hệ thống (Lưu Cookie)
                    // Đăng nhập hệ thống (Lưu Cookie)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Kiểm tra ReturnUrl nếu có
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    // 🎯 ĐIỀU HƯỚNG THEO VAI TRÒ (ROLE)
                    if (user.Role == "Admin")
                    {
                        // Chuyển hướng Admin vào trang quản trị trong Area Admin
                        return RedirectToAction("Index", "AdminProduct", new { area = "Admin" });
                    }

                    // Khách hàng thông thường chuyển về trang chủ xem sản phẩm
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
            }

            return View(model);
        }

        // ================= ĐĂNG XUẤT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}