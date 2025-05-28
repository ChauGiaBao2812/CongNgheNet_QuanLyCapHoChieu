using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


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

        public IActionResult Information(string? formID)
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
                ViewBag.AlertMessage = "Không có công dân mang CCCD này trong dữ liệu quốc gia";
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

            ViewBag.AlertMessage = $"Đơn của người đề nghị đã được gửi thành công. " +
                $"Vui lòng tải phiếu xác nhận ở bên dưới.";
            ViewBag.FormId = formId;

            ModelState.Clear();

            // Redirect hoặc trả về view thành công
            return View();
        }

        public IActionResult DownloadPdf(string formId)
        {
            byte[] pdfBytes = GenerateFormPdf(formId);

            string fileName = $"HoSo_{formId}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }

        private byte[] GenerateFormPdf(string formId)
        {
            var data = _context.PassportDatas.FirstOrDefault(p => p.FormID == formId);
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            string? Decrypt(byte[]? val) => val == null ? null : AesEcbEncryption.DecryptAesEcb(val);

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().AlignCenter().Text("CỤC QUẢN LÝ XUẤT NHẬP CẢNH")
                        .FontSize(14).Bold().FontColor(Colors.Red.Darken2);

                    page.Content().Background(Colors.Grey.Lighten4).Padding(20).Column(column =>
                    {
                        column.Spacing(15);

                        column.Item().AlignCenter().Text("PHIẾU XÁC NHẬN ĐĂNG KÝ HỘ CHIẾU")
                            .FontSize(22).Bold();

                        column.Item().AlignCenter().Text($"Mã hồ sơ: {formId}")
                            .FontSize(12).Italic();

                        //column.Item().PaddingTop(10).Text("Thông tin cá nhân").Bold().FontSize(18);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                            });

                            void Row(string label, string value)
                            {
                                table.Cell().Element(CellStyle).Text(label).Bold();
                                table.Cell().Element(CellStyle).Text(value ?? "");
                            }

                            IContainer CellStyle(IContainer container) =>
                                container.PaddingVertical(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        table.Cell().ColumnSpan(2).Element(CellStyle).Text("Thông tin cá nhân").Bold().FontSize(18);
                            Row("Họ tên", Decrypt(data.HoTen) ?? "");
                            Row("Giới tính", data.GioiTinh);
                            Row("Ngày sinh", data.NgaySinh.ToString("dd/MM/yyyy"));
                            Row("Nơi sinh", Decrypt(data.NoiSinh) ?? "");
                            Row("CCCD", Decrypt(data.CCCD) ?? "");
                            Row("Ngày cấp", data.NgayCap.ToString("dd/MM/yyyy"));
                            Row("Nơi cấp", Decrypt(data.NoiCap) ?? "");
                            Row("Dân tộc", data.DanToc);
                            Row("Tôn giáo", data.TonGiao ?? "");
                            Row("SĐT", Decrypt(data.SĐT) ?? "");
                            Row("Email", Decrypt(data.Email) ?? "");

                            table.Cell().ColumnSpan(2).Element(CellStyle).Text("Địa chỉ thường trú").Bold().FontSize(18); ;
                            Row("Tỉnh/Thành", Decrypt(data.ttTinhThanh) ?? "");
                            Row("Quận/Huyện", Decrypt(data.ttQuanHuyen) ?? "");
                            Row("Phường/Xã", Decrypt(data.ttPhuongXa) ?? "");
                            Row("Số nhà, Đường", Decrypt(data.ttSoNhaDuong) ?? "");

                            table.Cell().ColumnSpan(2).Element(CellStyle).Text("Địa chỉ tạm trú").Bold().FontSize(18); ;
                            Row("Tỉnh/Thành", Decrypt(data.thtTinhThanh) ?? "");
                            Row("Quận/Huyện", Decrypt(data.thtQuanHuyen) ?? "");
                            Row("Phường/Xã", Decrypt(data.thtPhuongXa) ?? "");
                            Row("Số nhà, Đường", Decrypt(data.thtSoNhaDuong) ?? "");

                            table.Cell().ColumnSpan(2).Element(CellStyle).Text("Thông tin cha mẹ").Bold().FontSize(18); ;
                            Row("Họ tên Cha", Decrypt(data.HoTenCha) ?? "");
                            Row("Ngày sinh Cha", data.NgaySinhCha?.ToString("dd/MM/yyyy") ?? "");
                            Row("Họ tên Mẹ", Decrypt(data.HoTenMe) ?? "");
                            Row("Ngày sinh Mẹ", data.NgaySinhMe?.ToString("dd/MM/yyyy") ?? "");

                            table.Cell().ColumnSpan(2).Element(CellStyle).Text("Thông tin hồ sơ").Bold().FontSize(18); ;
                            Row("Nội dung đề nghị", data.NoiDungDeNghi);
                            Row("Nơi tiếp nhận hồ sơ", data.NoiTiepNhanHS);
                            Row("Ngày nộp", data.NgayNop.ToString("dd/MM/yyyy"));
                        });

                        column.Item().PaddingTop(30).Row(row =>
                        {
                            row.RelativeItem().AlignCenter().Text("Người đăng ký").Bold().FontSize(18); ;
                            row.RelativeItem().AlignCenter().Text("Xác nhận cơ quan").Bold().FontSize(18); ;
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Element(c => c.PaddingTop(1).LineHorizontal(1));
                            row.RelativeItem().Element(c => c.PaddingTop(1).LineHorizontal(1));
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().AlignCenter().Text(Decrypt(data.HoTen) ?? "").FontSize(16).Italic();
                            row.RelativeItem();
                        });

                    });

                    page.Footer().Element(footer =>
                    {
                        footer.AlignCenter().Text(x =>
                        {
                            x.Span("Trang ");
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages().FontSize(10); ;
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}
