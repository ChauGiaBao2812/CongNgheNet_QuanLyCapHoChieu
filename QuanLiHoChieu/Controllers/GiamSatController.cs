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
using Microsoft.AspNetCore.Antiforgery;
using QuanLiHoChieu.Services.Interface;

namespace QuanLiHoChieu.Controllers
{
    [Authorize(Roles = "GiamSat")]
    public class GiamSatController : Controller
    {
        private readonly PassportDbContext _context;
        private readonly ILogger<GiamSatController> _logger;
        private readonly IRootAdminService _rootAdminService;

        public GiamSatController(PassportDbContext context, ILogger<GiamSatController> logger, IRootAdminService rootAdminService)
        {
            _context = context;
            _logger = logger;
            _rootAdminService = rootAdminService;
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

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(User?.Identity?.Name))
            {
                return RedirectToAction("AccessDenied", "Chung");
            }

            LoadUserGender();

            return View();
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

        public IActionResult Update(string? userId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DecryptedUserVM model)
        {
            LoadUserGender();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = _context.Users.SingleOrDefault(u => u.UserID == model.UserID);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View(model);
                }

                var currentUserId = User.FindFirst("UserID")?.Value ?? "Unknown";
                bool currentlyGiamSat = user.ChucVu == "GiamSat";
                bool demotingFromGiamSat = currentlyGiamSat && model.ChucVu != "GiamSat";
                bool demotingSelf = model.UserID == currentUserId && currentlyGiamSat && model.ChucVu != "GiamSat";

                if (demotingFromGiamSat)
                {
                    int remainingGiamSatCount = _context.Users.Count(u => u.ChucVu == "GiamSat" && u.UserID != model.UserID);

                    if (remainingGiamSatCount == 0)
                    {
                        ViewBag.AlertMessage = "Không thể thay đổi chức vụ Giám sát cuối cùng";
                        return View(model);
                    }
                }

                var sql = "EXEC sp_UpdateUser @UserID, @HoTen, @NgaySinh, @QueQuan, @SDT, @Email, @ChucVu";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@UserID", model.UserID),
                    new SqlParameter("@HoTen", model.HoTen),
                    new SqlParameter("@NgaySinh", model.NgaySinh),
                    new SqlParameter("@QueQuan", model.QueQuan),
                    new SqlParameter("@SDT", model.SDT),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("ChucVu", model.ChucVu)
                );

                if (demotingSelf)
                {
                    return RedirectToAction("Logout");
                }


                ViewBag.AlertMessage = "Cập nhật thông tin thành công!";

                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception here if you have a logger, e.g. _logger.LogError(ex, "Error updating user");

                ModelState.AddModelError("", $"Có lỗi xảy ra khi cập nhật thông tin. Vui lòng thử lại. {ex}");
                ViewBag.AlertMessage = $"{ex}";

                return View(model);
            }

        }

        public async Task<IActionResult> Delete(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("UserList");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.Username == username);

            if (user != null && user.ChucVu == "GiamSat")
            {
                TempData["AlertMessage"] = "Không thể xoá người dùng có chức vụ Giám sát";
                return RedirectToAction("UserList");
            }

            if (user != null)
            {
                _context.Users.Remove(user);
            }

            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }

            await _context.SaveChangesAsync();

            TempData["AlertMessage"] = "Xóa người dùng thành công!";
            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> ToggleActivate(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                TempData["AlertMessage"] = "Tên người dùng không hợp lệ.";
                return RedirectToAction("UserList");
            }

            var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.Username == username);

            if (taiKhoan == null)
            {
                TempData["AlertMessage"] = "Không tìm thấy tài khoản.";
                return RedirectToAction("UserList");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && user.ChucVu == "GiamSat")
            {
                TempData["AlertMessage"] = "Không thể vô hiệu hóa tài khoản của Giám sát.";
                return RedirectToAction("UserList");
            }

            taiKhoan.Activated = !taiKhoan.Activated;
            await _context.SaveChangesAsync();

            TempData["AlertMessage"] = taiKhoan.Activated
                ? "Tài khoản đã được kích hoạt lại."
                : "Tài khoản đã bị vô hiệu hóa.";

            return RedirectToAction("UserList");
        }

        public IActionResult UserList()
        {
            var sql = "EXEC sp_SelectUser";
            var users = _context.Set<DecryptedUserVM>().FromSqlRaw(sql).ToList();

            var activations = _context.TaiKhoans
                .Where(t => users.Select(u => u.Username).Contains(t.Username))
                .ToDictionary(t => t.Username, t => t.Activated);

            foreach (var user in users)
            {
                if (user.Username != null && activations.TryGetValue(user.Username, out var activated))
                {
                    user.Activated = activated;
                }
            }

            LoadUserGender();

            ViewBag.AlertMessage = TempData["AlertMessage"];

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

            var userIds = logs
                .Select(l => l.UserID)
                .Where(id => id != null)
                .Distinct()
                .ToList();

            var sql = "EXEC sp_SelectUser";
            var users = _context.Set<DecryptedUserVM>()
                .FromSqlRaw(sql)
                .AsEnumerable()
                .Where(u => !string.IsNullOrEmpty(u.UserID))
                .ToDictionary(u => u.UserID!, u => u.HoTen ?? "");

            var xacThucLog = logs.FirstOrDefault(l => l.LoaiXuLy == "XacThuc");
            var xetDuyetLog = logs.FirstOrDefault(l => l.LoaiXuLy == "XetDuyet");
            var luuTruLog = logs.FirstOrDefault(l => l.LoaiXuLy == "LuuTru");

            var model = new FormStatusVM
            {
                FormID = passport.FormID,
                NgayNop = passport.NgayNop,

                UserXacThucID = xacThucLog?.UserID,
                UserXacThucName = xacThucLog != null && xacThucLog.UserID != null && users.ContainsKey(xacThucLog.UserID)
                                  ? users[xacThucLog.UserID]
                                  : null,
                NoteXacThuc = xacThucLog?.GhiChu,

                UserXetDuyetID = xetDuyetLog?.UserID,
                UserXetDuyetName = xetDuyetLog != null && xetDuyetLog.UserID != null && users.ContainsKey(xetDuyetLog.UserID)
                                  ? users[xetDuyetLog.UserID]
                                  : null,
                NoteXetDuyet = xetDuyetLog?.GhiChu,

                UserLuuTruID = luuTruLog?.UserID,
                UserLuuTruName = luuTruLog != null && luuTruLog.UserID != null && users.ContainsKey(luuTruLog.UserID)
                                  ? users[luuTruLog.UserID]
                                  : null
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
