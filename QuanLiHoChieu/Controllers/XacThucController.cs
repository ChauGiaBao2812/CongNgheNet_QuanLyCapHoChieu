using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;

namespace QuanLiHoChieu.Controllers
{
    public class XacThucController : Controller
    {
        private readonly PassportDbContext _context;
        public XacThucController(PassportDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var xacThuc = await _context.PassportDatas.ToListAsync();
            return View(xacThuc);
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

    }
}
