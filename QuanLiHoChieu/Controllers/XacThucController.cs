using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "XacThuc")]
    public class XacThucController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<XacThucController> _logger;
        public XacThucController(PassportDbContext context, ILogger<XacThucController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IActionResult> List(string? sortMode)
        {
            var query = _context.PassportDatas
            .Select(passport => new
            {
                passport.FormID,
                passport.NgayNop,
                XuLy = _context.XuLys
                    .Where(x => x.FormID == passport.FormID && x.LoaiXuLy == "XacThuc")
                    .OrderByDescending(x => x.NgayXuLy)
                    .FirstOrDefault()
            });

            var result = await query.Select(r => new PassportStatusListVM
            {
                FormID = r.FormID,
                NgayNop = r.NgayNop,
                TrangThai = r.XuLy == null ? "Chưa xác thực"
                         : r.XuLy.TrangThai == "Verified" ? "Đã xác thực"
                         : r.XuLy.TrangThai == "Rejected" ? "Từ chối"
                         : "Không rõ"
            }).ToListAsync();

            LoadUserGender();

            return View(result);
        }
        public async Task<IActionResult> DetailProfile(string formId)
        {
            if (string.IsNullOrEmpty(formId))
            {
                return NotFound();
            }

            // Lấy PassportData kèm ResidentData qua Include (Eager Loading)
            var profile = await _context.PassportDatas
                .Include(p => p.ResidentData)   // Bao gồm dữ liệu ResidentData liên kết
                .FirstOrDefaultAsync(p => p.FormID == formId);

            if (profile == null)
            {
                return NotFound();
            }

            // Trả về view kèm model PassportData (có ResidentData bên trong)
            return View(profile);
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
