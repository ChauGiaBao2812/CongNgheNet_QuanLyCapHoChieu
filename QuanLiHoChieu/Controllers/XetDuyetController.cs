using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;
using QuanLiHoChieu.Services.Interface;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "XetDuyet")]
    public class XetDuyetController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<XetDuyetController> _logger;
        private readonly IGetDataByFormIdService _getDataService;

        public XetDuyetController(PassportDbContext context, ILogger<XetDuyetController> logger, IGetDataByFormIdService getDataService)
        {
            _context = context;
            _logger = logger;
            _getDataService = getDataService;
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
        public async Task<IActionResult> Review(string formId)
        {
            var passportData = await _getDataService.GetPassportVMByFormIdAsync(formId);

            var model = new XetDuyetFormCompositeVM
            {
                PassportData = passportData,
                Verification = new XacThucHoSoVM
                {
                    FormID = formId
                }
            };

            LoadUserGender();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(XetDuyetFormCompositeVM model, string TrangThai)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("List");
            }

            var xuLy = new XuLy
            {
                FormID = model.Verification!.FormID,
                GhiChu = model.Verification.GhiChu,
                TrangThai = TrangThai,
                UserID = User.FindFirst("UserID")?.Value ?? "Unknown",
                NgayXuLy = DateTime.Now,
                LoaiXuLy = "XetDuyet"
            };

            _context.XuLys.Add(xuLy);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Home", "Chung");
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
