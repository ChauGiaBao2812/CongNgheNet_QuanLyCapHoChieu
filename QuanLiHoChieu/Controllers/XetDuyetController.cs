using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "XetDuyet")]
    public class XetDuyetController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<XetDuyetController> _logger;

        public XetDuyetController(PassportDbContext context, ILogger<XetDuyetController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> List()
        {
            var verifiedForms = _context.XuLys
                .Where(x => x.LoaiXuLy == "XacThuc" && x.TrangThai == "Verified")
                .Select(x => x.FormID);

            var query = _context.PassportDatas
                .Where(p => verifiedForms.Contains(p.FormID))
                .Select(passport => new
                {
                    passport.FormID,
                    passport.NgayNop,
                    XetDuyet = _context.XuLys
                        .Where(x => x.FormID == passport.FormID && x.LoaiXuLy == "XetDuyet")
                        .OrderByDescending(x => x.NgayXuLy)
                        .FirstOrDefault()
                });

            var result = await query.Select(r => new PassportStatusListVM
            {
                FormID = r.FormID,
                NgayNop = r.NgayNop,
                TrangThai = r.XetDuyet == null ? "Chưa xét duyệt"
                         : r.XetDuyet.TrangThai == "Verified" ? "Đã xét duyệt"
                         : "Từ chối"
            }).ToListAsync();

            LoadUserGender();

            return View(result);
        }
        public IActionResult DetailProfile()
        {
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
                    ViewData["Ten"] = user.Username;
                    ViewData["GioiTinh"] = user.GioiTinh;
                }
            }
        }
    }
}
