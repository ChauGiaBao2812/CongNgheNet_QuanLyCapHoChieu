using Microsoft.AspNetCore.Mvc;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers
{
    public class PassportController : Controller
    {
        private PassportDbContext _context;
        private ILogger<PassportController> _logger;

        public PassportController(PassportDbContext context, ILogger<PassportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(PassportFormVM model, IFormFile? avatar)
        {
            if (!ModelState.IsValid)
            {
                // Log lỗi validation
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogWarning("Validation error in field '{Field}': {ErrorMessage}", entry.Key, error.ErrorMessage);
                    }
                }
                return View(model);
            }

            byte[] Encrypt(string? val) => val == null ? null! : AesEcbEncryption.EncryptAesEcb(val);

            // Mã hóa CCCD để truy vấn ResidentData
            var encryptedCCCD = Encrypt(model.CCCD);

            // Kiểm tra ResidentData đã tồn tại chưa
            var resident = await _context.ResidentDatas.FindAsync(encryptedCCCD);

            if (resident == null)
            {
                return View(model);
            }

            // Tạo FormID cho PassportData
            var formId = Guid.NewGuid().ToString("N").Substring(0, 20);

            // Tạo PassportData mới
            var passportData = new PassportData
            {
                FormID = formId,
                HoTen = Encrypt(model.HoTen),
                GioiTinh = model.GioiTinh,
                NgaySinh = model.NgaySinh,
                NoiSinh = Encrypt(model.NoiSinh),
                CCCD = encryptedCCCD,
                NgayCap = model.NgayCap,
                NoiCap = Encrypt(model.NoiCap),
                DanToc = model.DanToc,
                TonGiao = model.TonGiao,
                SĐT = Encrypt(model.SDT),
                Email = Encrypt(model.Email),
                Hinh = "",
                ttTinhThanh = Encrypt(model.ttTinhThanh),
                ttQuanHuyen = Encrypt(model.ttQuanHuyen),
                ttPhuongXa = Encrypt(model.ttPhuongXa),
                ttSoNhaDuong = Encrypt(model.ttSoNhaDuong),
                thtTinhThanh = Encrypt(model.thtTinhThanh),
                thtQuanHuyen = Encrypt(model.thtQuanHuyen),
                thtPhuongXa = Encrypt(model.thtPhuongXa),
                thtSoNhaDuong = Encrypt(model.thtSoNhaDuong),
                HoTenCha = model.HoTenCha != null ? Encrypt(model.HoTenCha) : null,
                NgaySinhCha = model.NgaySinhCha,
                HoTenMe = model.HoTenMe != null ? Encrypt(model.HoTenMe) : null,
                NgaySinhMe = model.NgaySinhMe,
                NoiDungDeNghi = "Đăng ký cấp hộ chiếu",  // Có thể lấy từ model nếu có thêm trường
                NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh", // Tùy chỉnh
                NgayNop = DateTime.Now
            };

            _context.PassportDatas.Add(passportData);
            await _context.SaveChangesAsync();

            // Redirect hoặc trả về view thành công
            return RedirectToAction("Success");  // Tạo view Success.cshtml để hiển thị kết quả
        }
    }
}
