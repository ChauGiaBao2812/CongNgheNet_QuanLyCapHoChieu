using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<FormController> _logger;

        public FormController(PassportDbContext context, ILogger<FormController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{formID}")]
        public async Task<IActionResult> GetStatus(string formID)
        {
            if (string.IsNullOrEmpty(formID))
            {
                return BadRequest(new { message = "Mã hồ sơ không được để trống." });
            }

            var forms = await _context.PassportDatas
                .Where(x => x.FormID == formID)
                .ToListAsync();

            if (!forms.Any())
            {
                return NotFound(new { message = "Không tìm thấy hồ sơ." });
            }

            var logs = await _context.XuLys
                .Where(x => x.FormID == formID)
                .ToListAsync();

            var xacThuc = logs.FirstOrDefault(x => x.LoaiXuLy == "XacThuc");
            var xetDuyet = logs.FirstOrDefault(x => x.LoaiXuLy == "XetDuyet");
            var luuTru = logs.FirstOrDefault(x => x.LoaiXuLy == "LuuTru");

            string status;

            if (luuTru != null && luuTru.TrangThai == "Verified")
                status = "Đã lưu vào danh sách hộ chiếu";
            else if (xetDuyet != null && xetDuyet.TrangThai == "Rejected")
                status = "Không đồng ý cấp hộ chiếu";
            else if (xacThuc != null && xacThuc.TrangThai == "Rejected")
                status = "Không đồng ý cấp hộ chiếu";
            else if (xetDuyet != null && xetDuyet.TrangThai == "Verified")
                status = "Đồng ý cấp hộ chiếu";
            else
                status = "Đang xử lý";

            return Ok(new
            {
                formID = formID,
                status = status
            });

        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetFormStats()
        {
            var currentYear = DateTime.Now.Year;

            var forms = await _context.PassportDatas
                .Where(p => p.NgayNop.Year == currentYear)
                .ToListAsync();

            var formIds = forms.Select(f => f.FormID).ToList();

            var xuLys = await _context.XuLys
                .Where(x => formIds.Contains(x.FormID))
                .ToListAsync();

            int total = forms.Count;
            int resolved = 0;
            int onTime = 0;

            foreach (var form in forms)
            {
                var logs = xuLys.Where(x => x.FormID == form.FormID).ToList();

                bool isRejected = logs.Any(l => l.TrangThai == "Rejected");
                bool isApproved = new[] { "XacThuc", "XetDuyet", "LuuTru" }
                    .All(role => logs.Any(l => l.TrangThai == "Verified" && l.LoaiXuLy == role));

                if (isRejected || isApproved)
                {
                    resolved++;

                    // NgayXuLy cuối cùng
                    var latestHandledDate = logs.Max(x => x.NgayXuLy);
                    var duration = (latestHandledDate - form.NgayNop).TotalDays;

                    if (duration <= 5)
                        onTime++;
                }
            }   

            return Ok(new
            {
                Tong = total,
                DaGiaiQuyet = resolved,
                DungHan = onTime
            });
        }
    }
}
