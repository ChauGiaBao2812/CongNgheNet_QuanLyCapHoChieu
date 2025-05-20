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

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
