using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Models.ViewModels;
using QuanLiHoChieu.Models;
using QuanLiHoChieu.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "GiamSat")]
    public class GiamSatController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<GiamSatController> _logger;

        public GiamSatController(PassportDbContext context, ILogger<GiamSatController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(User?.Identity?.Name))
            {
                return RedirectToAction("AccessDenied", "Chung");
            }

            LoadUserGender();

            return View();
        }

        public IActionResult ActionHistory()
        {
            var model = _context.XuLys
                .Include(x => x.User)
                .ToList();

            LoadUserGender();

            return View(model);
        }

        public async Task<IActionResult> UserActionDetail(string? userId)
        {
            LoadUserGender();

            var handledFormIDs = new List<string>();
            if (!string.IsNullOrEmpty(userId))
            {
                handledFormIDs = await _context.XuLys
                    .Where(x => x.UserID == userId)
                    .Select(x => x.FormID)
                    .Distinct()
                    .ToListAsync();
            }

            var formQuery = _context.PassportDatas.AsQueryable();
            if (!string.IsNullOrEmpty(userId))
            {
                formQuery = formQuery.Where(p => handledFormIDs.Contains(p.FormID));
            }

            var passportForms = await formQuery.ToListAsync();

            var formIDs = passportForms.Select(p => p.FormID).ToList();
            var xuLyLogs = await _context.XuLys
                .Where(x => formIDs.Contains(x.FormID))
                .ToListAsync();

            var formStatusList = passportForms.Select(form =>
            {
                var formLogs = xuLyLogs.Where(l => l.FormID == form.FormID).ToList();

                bool? xacThucStatus = GetStatus(formLogs, "XacThuc");
                bool? xetDuyetStatus = GetStatus(formLogs, "XetDuyet");
                bool? luuTruStatus = GetStatus(formLogs, "LuuTru");

                return new FormStatusVM
                {
                    FormID = form.FormID,
                    XacThucStatus = xacThucStatus,
                    XetDuyetStatus = xetDuyetStatus,
                    LuuTruStatus = luuTruStatus,
                    NgayNop = form.NgayNop
                };
            }).ToList();

            return View(formStatusList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoanUserViewModel model)
        {
            LoadUserGender();

            model.UserID = Guid.NewGuid().ToString("N").Substring(0, 20);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username already exists
            if (_context.TaiKhoans.Any(t => t.Username == model.Username))
            {
                ViewBag.AlertMessage = "Tạo tài khoản không thành công. Đã có tên tài khoản của người dùng này";
                ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                return View(model);
            }

            // Convert client-side hashed password hex string to byte[]
            var hashedPasswordBytes = TypeConvertHelper.ComputeSHA256(model.Password);

            var taiKhoan = new TaiKhoan
            {
                Username = model.Username,
                MatKhau = hashedPasswordBytes
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            // Call stored procedure to insert User data
            var sql = "EXEC sp_InsertUser @UserID, @HoTen, @GioiTinh, @NgaySinh, @QueQuan, @SDT, @Email, @ChucVu, @Username";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@UserID", model.UserID),
                new SqlParameter("@HoTen", model.HoTen),
                new SqlParameter("@GioiTinh", model.GioiTinh),
                new SqlParameter("@NgaySinh", model.NgaySinh),
                new SqlParameter("@QueQuan", model.QueQuan),
                new SqlParameter("@SDT", model.SDT),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@ChucVu", model.ChucVu),
                new SqlParameter("@Username", model.Username)
            );

            // Set a success message in ViewData or ViewBag
            ViewBag.AlertMessage = "Tạo tài khoản thành công!";

            ModelState.Clear();

            return View();
        }

        public IActionResult Detail(string? userId)
        {
            var sql = "EXEC sp_SelectUser";
            var users = _context.Set<DecryptedUserVM>()
                .FromSqlRaw(sql)
                .AsEnumerable();
            
            var user = users.FirstOrDefault(x => x.UserID == userId);

            if (user == null)
            {
                return View("NotFound");
            }

            LoadUserGender();

            return View(user);
        }

        public IActionResult UserList()
        {
            var sql = "EXEC sp_SelectUser";
            var users = _context.Set<DecryptedUserVM>().FromSqlRaw(sql).ToList();

            LoadUserGender();

            return View(users);
        }

        public async Task<IActionResult> FormDetail(string? formId)
        {
            if (string.IsNullOrEmpty(formId))
                return NotFound("Thiếu mã hồ sơ.");

            var passport = await _context.PassportDatas
                .FirstOrDefaultAsync(p => p.FormID == formId);

            if (passport == null)
                return NotFound("Không tìm thấy hồ sơ.");

            var logs = await _context.XuLys
                .Where(x => x.FormID == formId)
                .ToListAsync();

            var model = new FormStatusVM
            {
                FormID = passport.FormID,
                NgayNop = passport.NgayNop,
                UserXacThucID = logs.FirstOrDefault(l => l.LoaiXuLy == "XacThuc")?.UserID,
                NoteXacThuc = logs.FirstOrDefault(l => l.LoaiXuLy == "XacThuc")?.GhiChu,
                UserXetDuyetID = logs.FirstOrDefault(l => l.LoaiXuLy == "XetDuyet")?.UserID,
                NoteXetDuyet = logs.FirstOrDefault(l => l.LoaiXuLy == "XetDuyet")?.GhiChu,
                UserLuuTruID = logs.FirstOrDefault(l => l.LoaiXuLy == "LuuTru")?.UserID
            };

            LoadUserGender();

            return View(model);
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

        private bool? GetStatus(List<XuLy> logs, string loaiXuLy)
        {
            var xuLy = logs.FirstOrDefault(x => x.LoaiXuLy == loaiXuLy);

            if (xuLy == null)
                return null; // Not yet processed (pending)

            return xuLy.TrangThai switch
            {
                "Verified" => true,
                "Rejected" => false,
                _ => null
            };
        }
    }
}
