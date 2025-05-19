using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers
{
    public class ChungController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<ChungController> _logger;

        public ChungController(PassportDbContext context, ILogger<ChungController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Error here");
                // Có thể return lại View(model) và xử lý lỗi nếu muốn
                return View(model);
            }

            _logger.LogInformation(model.Password);

            // Xác thực đăng nhập
            var hashedPasswordBytes = TypeConvertHelper.HexStringToByteArray(model.Password);
            var account = await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.Username == model.Username && t.MatKhau == hashedPasswordBytes);

            if (account == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View(model);
            }

            // Lấy thông tin tài khoản liên quan
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == account.Username);

            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy người dùng";
                return View(model);
            }

            // Thêm Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, user.ChucVu), // Use ChucVu as role (e.g., "GiamSat")
                new Claim("UserID", user.UserID)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            _logger.LogInformation("User role is: {Role}", user.ChucVu);


            // Step 4: Đăng nhập với Cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false // This ensures the cookie is session-only, disappears when browser closes
            };

            _logger.LogInformation("Signing in with IsPersistent={IsPersistent}", authProperties.IsPersistent);

            await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

            return user.ChucVu switch
            {
                "GiamSat" => RedirectToAction("Create", "GiamSat"),
                "XacThuc" => RedirectToAction("Index", "Home"),  // Or a ComingSoon page
                "XetDuyet" => RedirectToAction("Index", "Home"),
                "LuuTru" => RedirectToAction("Index", "Home"),
                _ => RedirectToAction("Index", "Home")
            };

            //// Thành công -> tạo session hoặc chuyển hướng
            //return RedirectToAction("Index", "Home");
        }

        public IActionResult Forget()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}
