using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models.ViewModels;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "GiamSat")]
    public class GiamSatController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<GiamSatController> _logger;

        public GiamSatController(PassportDbContext context, ILogger<GiamSatController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(User?.Identity?.Name))
            {
                return RedirectToAction("AccessDenied", "Chung");
            }

            LoadUserGender();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoanUserViewModel model)
        {
            LoadUserGender();

            model.UserID = Guid.NewGuid().ToString("N").Substring(0, 20);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username already exists
            if (_context.TaiKhoans.Any(t => t.Username == model.Username))
            {
                ViewBag.AlertMessage = "Tạo tài khoản không thành công. Đã có tên tài khoản của người dùng này";
                ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                return View(model);
            }

            // Convert client-side hashed password hex string to byte[]
            var hashedPasswordBytes = TypeConvertHelper.ComputeSHA256(model.Password);

            var taiKhoan = new TaiKhoan
            {
                Username = model.Username,
                MatKhau = hashedPasswordBytes
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            // Call stored procedure to insert User data
            var sql = "EXEC sp_InsertUser @UserID, @HoTen, @GioiTinh, @NgaySinh, @QueQuan, @SDT, @Email, @ChucVu, @Username";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@UserID", model.UserID),
                new SqlParameter("@HoTen", model.HoTen),
                new SqlParameter("@GioiTinh", model.GioiTinh),
                new SqlParameter("@NgaySinh", model.NgaySinh),
                new SqlParameter("@QueQuan", model.QueQuan),
                new SqlParameter("@SDT", model.SDT),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@ChucVu", model.ChucVu),
                new SqlParameter("@Username", model.Username)
            );

            // Set a success message in ViewData or ViewBag
            ViewBag.AlertMessage = "Tạo tài khoản thành công!";

            ModelState.Clear();

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Chung");
        }

        private void LoadUserGender()
        {
            var username = User.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    ViewData["GioiTinh"] = user.GioiTinh;
                }
            }
        }
    }
}
