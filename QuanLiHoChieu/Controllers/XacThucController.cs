using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;
using QuanLiHoChieu.Services.Interface;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "XacThuc")]
    public class XacThucController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<XacThucController> _logger;
        private readonly IGetDataByFormIdService _getDataService;
        public XacThucController(PassportDbContext context, ILogger<XacThucController> logger, IGetDataByFormIdService getDataService)
        {
            _context = context;
            _logger = logger;
            _getDataService = getDataService;

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
        public async Task<IActionResult> Verify(string formId)
        {
            if (string.IsNullOrEmpty(formId))
                return NotFound();

            var passportData = await _getDataService.GetPassportResidentVMByFormIdAsync(formId);

            var model = new XacThucFormCompositeVM
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
        public async Task<IActionResult> Verify(XacThucFormCompositeVM model, string TrangThai)
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
                LoaiXuLy = "XacThuc"
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
