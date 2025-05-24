using System.Linq;
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
                ViewBag.AlertMessage = "Thiếu dữ liệu. Mời người đề nghị kiểm tra lại.";
                return View(model);
            }

            var GUID = Guid.NewGuid().ToString("N").Substring(0, 20);

            byte[] Encrypt(string? val) => val == null ? null! : AesEcbEncryption.EncryptAesEcb(val);

            // Mã hóa CCCD để truy vấn ResidentData
            var encryptedCCCD = Encrypt(model.CCCD);

            // Kiểm tra ResidentData đã tồn tại chưa
            var resident = await _context.ResidentDatas.FindAsync(encryptedCCCD);

            if (resident == null)
            {
                return View(model);
            }

            var newFileName = "";

            if (avatar == null)
            {
                _logger.LogInformation("Image Not Found");
                ViewBag.AlertMessage = "Thiếu hình ảnh. Mời người đề nghị thêm vào. ";
                return View(model);
            }
            else
            {
                _logger.LogInformation("Image Found");

                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(avatar.FileName).ToLower();

                if (allowedExtensions.Contains(fileExtension))
                {
                    newFileName = $"{GUID}{fileExtension}";

                    model.Hinh = newFileName;

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", newFileName);
                    using (var fs = new FileStream(fullPath, FileMode.CreateNew))
                    {
                        avatar.CopyTo(fs);
                    }
                }
            }

            // Tạo FormID cho PassportData
            var formId = GUID;

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
                Hinh = newFileName,
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
                NoiTiepNhanHS = "Hệ thống Tiếp nhận thông tin hộ chiếu Chính phủ", // Tùy chỉnh
                NgayNop = DateTime.Now
            };

            _context.PassportDatas.Add(passportData);
            await _context.SaveChangesAsync();

            ViewBag.AlertMessage = $"Đơn của người đề nghị đã được gửi thành công. Mã đơn: {formId}";

            ModelState.Clear();

            // Redirect hoặc trả về view thành công
            return View();
        }
    }
}
