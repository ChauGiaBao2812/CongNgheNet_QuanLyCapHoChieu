using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models.ViewModels;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Data;

namespace QuanLiHoChieu.Controllers
{
    public class GiamSatController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<GiamSatController> _logger;

        public GiamSatController(PassportDbContext context, ILogger<GiamSatController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoanUserViewModel model)
        {
            model.UserID = Guid.NewGuid().ToString("N").Substring(0, 20);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username already exists
            if (_context.TaiKhoans.Any(t => t.Username == model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                return View(model);
            }

            // Convert client-side hashed password hex string to byte[]
            var hashedPasswordBytes = TypeConvertHelper.HexStringToByteArray(model.Password);

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

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
