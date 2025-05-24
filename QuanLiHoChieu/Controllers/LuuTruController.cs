using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Models.ViewModels;


namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "LuuTru")]
    public class LuuTruController : Controller
    {
        private readonly PassportDbContext _context;

        public LuuTruController(PassportDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var xuLyLogs = await _context.XuLys.ToListAsync(); // Materialize first

            var statusList = xuLyLogs
                .GroupBy(x => x.FormID)
                .Select(g =>
                {
                    var xacThuc = g.FirstOrDefault(x => x.LoaiXuLy == "XacThuc");
                    var xetDuyet = g.FirstOrDefault(x => x.LoaiXuLy == "XetDuyet");
                    var luuTru = g.FirstOrDefault(x => x.LoaiXuLy == "LuuTru");

                    string status;

                    if (luuTru != null && luuTru.TrangThai == "Verified")
                    {
                        status = "Đã lưu vào danh sách hộ chiếu";
                    }
                    else if (xetDuyet != null && xetDuyet.TrangThai == "Rejected")
                    {
                        status = "Không đồng ý cấp hộ chiếu";
                    }
                    else if (xacThuc != null && xacThuc.TrangThai == "Rejected")
                    {
                        status = "Không đồng ý cấp hộ chiếu";
                    }
                    else if (xetDuyet != null && xetDuyet.TrangThai == "Verified")
                    {
                        status = "Đồng ý cấp hộ chiếu";
                    }
                    else
                    {
                        // Still under review or not enough info
                        return null;
                    }

                    return new LuuTruVM
                    {
                        formID = g.Key,
                        Status = status
                    };
                })
                .Where(x => x != null)
                .ToList();

            LoadUserGender();

            return View(statusList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store(string formID){
            var userID = User.FindFirst("UserID")?.Value ?? "Unknown";

            var xuLy = new XuLy
            {
                FormID = formID,
                GhiChu = null,
                TrangThai = "Verified",
                UserID = userID,
                NgayXuLy = DateTime.Now,
                LoaiXuLy = "LuuTru"
            };

            _context.XuLys.Add(xuLy);

            var passportData = await _context.PassportDatas
                .Where(pd => pd.FormID == formID)
                .Select(pd => new { pd.NgayNop })
                .FirstOrDefaultAsync();

            var luuTru = new LuuTru
            {
                PassportID = Guid.NewGuid().ToString("N").Substring(0, 20),
                UserID = userID,
                FormID = formID,
                NgayNop = passportData!.NgayNop,
                NgayCap = DateTime.Now,
                CoGiaTriDen = DateTime.Now.AddYears(10)
            };

            _context.LuuTrus.Add(luuTru);

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
