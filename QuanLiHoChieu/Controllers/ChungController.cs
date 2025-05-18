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
        [HttpPost]
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

            // Thành công -> tạo session hoặc chuyển hướng
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
