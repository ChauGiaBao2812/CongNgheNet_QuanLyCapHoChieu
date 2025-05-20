using Microsoft.AspNetCore.Mvc;
using QuanLiHoChieu.Data;

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
            return View();
        }
        public async Task<IActionResult> DetailProfile()
        {
            return View();
        }
    }
}
