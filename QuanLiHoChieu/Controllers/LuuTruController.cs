using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models;


namespace QuanLiHoChieu.Controllers
{
    public class LuuTruController : Controller
    {
        private readonly PassportDbContext _context;

        public LuuTruController(PassportDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var luuTru = await _context.LuuTrus.ToListAsync();
            return View(luuTru);
        }
    }
}
